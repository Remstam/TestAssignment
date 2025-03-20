using System.Collections.Generic;

namespace TestGame
{
    public class CameraSystem : BaseSystem
    {
        private readonly InputSystem _inputSystem;
        private readonly PlayerController _playerController;
        private readonly CameraController _cameraController;

        public CameraSystem(InputSystem inputSystem, PlayerController playerController, CameraController cameraController)
        {
            _inputSystem = inputSystem;
            _playerController = playerController;
            _cameraController = cameraController;
            
            _inputSystem.CameraKeyboardSubscriber.OnKeyActions += OnCameraKeyActions;
            _inputSystem.PlayerKeyboardSubscriber.OnKeyActions += OnPlayerKeyActions;
        }

        public void Init()
        {
            _cameraController.Init(_playerController.transform);
        }
        
        private void OnPlayerKeyActions(List<KeyAction> actions)
        {
            foreach (var action in actions)
            {
                if (action is KeyAction.MoveLeft or KeyAction.MoveRight or KeyAction.MoveUp or KeyAction.MoveDown)
                {
                    _cameraController?.UpdateFollowCamera();
                }
            }
        }

        private void OnCameraKeyActions(List<KeyAction> actions)
        {
            foreach (var action in actions)
            {
                if (action == KeyAction.SwitchCamera)
                {
                    _cameraController?.SwitchCamera();
                }
            }
        }

        public override void Dispose()
        {
            _inputSystem.CameraKeyboardSubscriber.OnKeyActions -= OnCameraKeyActions;
            _inputSystem.PlayerKeyboardSubscriber.OnKeyActions -= OnPlayerKeyActions;
        }
    }
}