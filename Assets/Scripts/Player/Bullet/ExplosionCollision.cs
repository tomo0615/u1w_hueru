using DG.Tweening;
using EffectManager;
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

        [Inject] private GameEffectManager _gameEffectManager;
        
        private bool _isHitEnemy = false;
        
        public void Explosion()
        {
            _gameEffectManager.OnGenelateEffect(transform.position, EffectType.Explosion);
            
            transform.DOScale(Vector3.one*1.5f, 0.1f)
                .OnComplete(() =>
                {
                    if (_isHitEnemy == false)
                    {
                        _enemySpawner.InstanceRandomEnemy(transform.position);
                    }
                    
                    Destroy(gameObject);
                });
            
            this.OnCollisionEnter2DAsObservable()
                .Select(damageable => damageable.gameObject.GetComponent<IDamageable>())
                .Where(damageable => damageable != null)
                .Subscribe(damageable =>
                {
                    damageable.ApplyDamage();
                    _isHitEnemy = true;
                });
        }
    }
}
