using DG.Tweening;
using EffectManager;
using Enemy.Spawner;
using Interfaces;
using Sound;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using Zenject;

namespace Player.Bullet
{
    public class ExplosionCollision : MonoBehaviour
    {
        [Inject] private EnemySpawner _enemySpawner;
        
        [Inject] private AudioManager _audioManager;
        
        private bool _isHitEnemy = false;

        private const float MaxScale = 1.5f;
        
        public void Explosion(float chargePower)
        {
            _audioManager.PlaySE(SEType.Explosion);
            
            transform.DOScale(Vector3.one * GetChargeRation(chargePower), 0.1f)
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

        private float GetChargeRation(float chargePower)
        {
            return MaxScale * chargePower;
        }
    }
}
