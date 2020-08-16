using GameEnd;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using Zenject;

namespace Enemy.Spawner
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private EnemySpawnTable enemySpawnTable;

        [SerializeField] private int maxRandomCount = 3;

        private int _currentEnemyCount = 0;

        [Inject] private GameEndPresenter _gameEndPresenter;

        [SerializeField] private Transform firstChaseTransform;

        [SerializeField] private Transform firstEscapeTransform;

        private void Start()
        {
            InstanceEnemy(1, firstEscapeTransform.position);

            InstanceEnemy(0,firstChaseTransform.position);
            
            this.UpdateAsObservable()
                .Where(_ => _currentEnemyCount <= 0)
                .Subscribe(_ =>
                {
                    _gameEndPresenter.OnGameEnd(true);
                });
        }


        private void InstanceEnemy(int index, Vector3 spawnPosition)
        { 
            Instantiate(enemySpawnTable.EnemyList[index], spawnPosition, Quaternion.identity);
            _currentEnemyCount++;
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
            _currentEnemyCount--;
        }
    }
}
