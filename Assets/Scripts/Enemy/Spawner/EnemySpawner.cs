using GameEnd;
using UniRx;
using UnityEngine;
using Zenject;

namespace Enemy.Spawner
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private EnemySpawnTable enemySpawnTable;

        [SerializeField] private int maxRandomCount = 3;

        private ReactiveProperty<int> _currentEnemyCount = new ReactiveProperty<int>();
        
        [Inject] private GameEndPresenter _gameEndPresenter;

        [SerializeField] private Transform firstChaseTransform;

        [SerializeField] private Transform firstEscapeTransform;
        
        public void Initialize()
        {
            InstanceEnemy(1, firstEscapeTransform.position);

            InstanceEnemy(0,firstChaseTransform.position);
            
            _currentEnemyCount
                .Where(value => value <= 0)
                .Subscribe(_ =>
                {
                    _gameEndPresenter.OnGameEnd(true);
                });
        }


        private void InstanceEnemy(int index, Vector3 spawnPosition)
        { 
            Instantiate(enemySpawnTable.EnemyList[index], spawnPosition, Quaternion.identity);
            _currentEnemyCount.Value++;
        }

        //攻撃を外したら呼び出す
        public void InstanceRandomEnemy(Vector3 spawnPosition)
        {
            var randomCount = Random.Range(1, maxRandomCount+1);

            for (var i = 0; i < randomCount; i++)
            {
                var randomIndex = Random.Range(0, enemySpawnTable.EnemyList.Count);
                
                spawnPosition = GetInstancePosition(spawnPosition);

                InstanceEnemy(randomIndex, spawnPosition);
            }
        }
        
        private Vector3 GetInstancePosition(Vector3 position)
        {
            var random = Random.Range(0f, 360f);
            var theta = random * Mathf.PI / 180f;
            var x = Mathf.Cos(theta) + position.x;
            var y = Mathf.Sin(theta) + position.y;
            return new Vector3(x, y, 0f);
        }

        public void DecreaseEnemy()
        {
            _currentEnemyCount.Value--;
        }
    }
}
