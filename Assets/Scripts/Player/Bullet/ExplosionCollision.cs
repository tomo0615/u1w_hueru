using DG.Tweening;
using Enemy.Spawner;
using Interfaces;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using Zenject;

namespace Player.Bullet
{
    public class ExplosionCollision : MonoBehaviour
    {
        [Inject] private EnemySpawner _enemySpawner;

        private bool _isEnemyHit = false;
        
        public void Explosion()
        {
            transform.DOScale(Vector3.one, 0.1f)
                .OnComplete(() => Destroy(gameObject));
            
            //EnemyHit
            this.OnCollisionEnter2DAsObservable()
                .Select(damageable => damageable.gameObject.GetComponent<IDamageable>())
                .Where(damageable => damageable != null)
                .Subscribe(damageable =>
                {
                    damageable.ApplyDamage();
                    _isEnemyHit = true;
                });
            
            //外した時
            this.OnCollisionEnter2DAsObservable()
                .Where(_ => _isEnemyHit == false)
                .Subscribe(damageable =>
                {
                    _enemySpawner.InstanceRandomEnemy(transform.position);
                });
        }
    }
}
