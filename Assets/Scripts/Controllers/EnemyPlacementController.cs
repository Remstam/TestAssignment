using UnityEngine;

namespace TestGame
{
    public class EnemyPlacementController : MonoBehaviour
    {
        [SerializeField] private Transform[] _spawnPoints;
        [SerializeField] private Transform _enemyAnchor;

        public void PlaceEnemy(GameObject enemyInstance)
        {
            var spawnPoint = _spawnPoints.GetRandom();
            enemyInstance.transform.SetParent(_enemyAnchor);
            enemyInstance.transform.position = spawnPoint.position;
        }
    }
}