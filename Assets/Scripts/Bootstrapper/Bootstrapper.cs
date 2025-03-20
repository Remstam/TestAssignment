using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace TestGame
{
    public class Bootstrapper : MonoBehaviour
    {
        [SerializeField] private InputListener _inputListener;
        [SerializeField] private PlayerController _playerController;
        [SerializeField] private CameraController _cameraController;
        [SerializeField] private EnemyPlacementController _enemyPlacementController;
        [SerializeField] private PoolsController _poolsController;
        [SerializeField] private FenceController _fenceController;
        [SerializeField] private DebugWindowController _debugWindowController;

        private List<BaseSystem> _systems;
        private void Awake()
        {
            Application.targetFrameRate = 60;
            
            ServiceContainer.Register<IInputListener>(_inputListener);
            ServiceContainer.Register(_playerController);
            ServiceContainer.Register(_cameraController);
            ServiceContainer.Register(_enemyPlacementController);
            ServiceContainer.Register(_poolsController);
            ServiceContainer.Register(_fenceController);
            ServiceContainer.Register(_debugWindowController);
        }

        private async void Start()
        {
            //TODO: init order is important
            var systemInitializers = new List<ISystemInitializer>()
            {
                new TimerSystemInitializer(),
                new ConfigSystemInitializer(),
                new InputSystemInitializer(),
                new FenceSystemInitializer(),
                new PlayerSystemInitializer(),
                new CameraSystemInitializer(),
                new PoolSystemInitializer(),
                new EnemySpawnSystemInitializer(),
                new EnemiesSystemInitializer(),
                new BattleSystemInitializer(),
                new TargetSystemInitializer(),
                new SpellsSystemInitializer(),
                new DebugSystemInitializer(),
                new DebugWindowInitializer()
            };
            
            _systems = await InitSystems(systemInitializers);
        }

        private async Task<List<BaseSystem>> InitSystems(List<ISystemInitializer> systemInitializers)
        {
            var systems = new List<BaseSystem>();

            foreach (var initer in systemInitializers)
            {
                var system = await initer.InitAsync();
                Debug.Log($"[{nameof(Bootstrapper)}] System inited: {initer.Type}");
                
                ServiceContainer.Register(initer.Type, system);
                
                systems.Add(system);
            }

            return systems;
        }
          
        private void OnDestroy()
        {
            foreach (var system in _systems)
            {
                system.Dispose();
            }

            if (Application.isEditor)
            {
                ServiceContainer.ClearContainer();
            }
        }
    }
}