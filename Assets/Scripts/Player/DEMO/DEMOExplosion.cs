using DG.Tweening;
using Interfaces;
using Sound;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using Zenject;

namespace Player.DEMO
{
    public class DEMOExplosion : MonoBehaviour
    {
        [Inject] private AudioManager _audioManager;
        
        private const float MaxScale = 1.5f;
        
        public void Explosion(float chargePower)
        {
            _audioManager.PlaySE(SEType.Explosion);

            transform.DOScale(Vector3.one * GetChargeRation(chargePower), 0.1f)
                .OnComplete(() =>
                {
                    Destroy(gameObject);
                });
            
            this.OnCollisionEnter2DAsObservable()
                .Select(damageable => damageable.gameObject.GetComponent<IDamageable>())
                .Where(damageable => damageable != null)
                .Subscribe(damageable =>
                {
                    damageable.ApplyDamage();
                });
        }

        private float GetChargeRation(float chargePower)
        {
            return MaxScale * chargePower;
        }
    }
}
