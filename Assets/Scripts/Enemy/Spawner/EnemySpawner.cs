using UnityEngine;

namespace Enemy.Spawner
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private EnemySpawnTable enemySpawnTable;

        [SerializeField] private int maxRandomCount = 3;
        private void Start()
        {
            InstanceEnemy();
        }
        
        //攻撃を外したら呼び出す
        public void InstanceEnemy()
        { 
            Instantiate(enemySpawnTable.EnemyList[0], transform.position, Quaternion.identity);
        }

        public void InstanceRandomEnemy(Vector3 spawnPosition)
        {
            var randomCount = Random.Range(1, maxRandomCount);

            for (var i = 0; i < randomCount; i++)
            {
                var randomIndex = Random.Range(0, enemySpawnTable.EnemyList.Count - 1);
                
                spawnPosition = GetInstancePosition(spawnPosition);

                Instantiate(enemySpawnTable.EnemyList[randomIndex], spawnPosition, Quaternion.identity);
            }
        }
        
        private Vector3 GetInstancePosition(Vector3 position)
        {
            var random = Random.Range(0f, 360f);
            var theta = random * Mathf.PI / 180f;
            var x = Mathf.Cos(theta) * 1 + position.x;
            var y = Mathf.Sin(theta) * 1 + position.y;
            return new Vector3(x, y, 0f);
        }
    }
}
