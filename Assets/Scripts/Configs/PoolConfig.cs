using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TestGame
{
    [CreateAssetMenu(fileName = "PoolConfig", menuName = "TestGame/Pool/Pool Config")]
    public class PoolConfig : ScriptableObject
    {
        [SerializeField] private List<PoolSettings> _poolSettings;

        public PoolSettings GetPoolSettingsByType(PoolType poolType)
        {
            return _poolSettings.FirstOrDefault(x => x.Type == poolType);
        }
    }
}