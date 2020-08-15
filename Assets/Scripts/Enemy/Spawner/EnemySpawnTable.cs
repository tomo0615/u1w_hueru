using System.Collections.Generic;
using UnityEngine;

namespace Enemy.Spawner
{
    [CreateAssetMenu(menuName = "DataTable/EnemySpawnTable", fileName = "EnemySpawnTable")]
    public class EnemySpawnTable : ScriptableObject
    {
        [SerializeField] private GameObject chaseEnemy = default;

        [SerializeField] private GameObject escapeEnemy = default;
        
        public List<GameObject> EnemyList =>
            new List<GameObject>
            {
                chaseEnemy,
                escapeEnemy
            };
    }
}
