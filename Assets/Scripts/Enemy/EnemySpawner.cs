using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private BaseEnemy chaseEnemy;

        [SerializeField] private List<Transform> spawnTransforms;

        private void Start()
        {
            SpawnEnemy();
        }

        private void SpawnEnemy()
        {
            foreach (var spawnTransform in spawnTransforms)
            {
                var enemy = Instantiate(chaseEnemy, spawnTransform.position, Quaternion.identity);
            }
        }
    }
}
