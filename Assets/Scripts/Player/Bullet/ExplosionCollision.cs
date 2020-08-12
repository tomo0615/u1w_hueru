using DG.Tweening;
using Interfaces;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace Player.Bullet
{
    public class ExplosionCollision : MonoBehaviour
    {
        public void Explosion()
        {
            transform.DOScale(Vector3.one, 0.1f)
                .OnComplete(() => Destroy(gameObject));
            
            this.OnCollisionEnter2DAsObservable()
                .Select(damageable => damageable.gameObject.GetComponent<IDamageable>())
                .Where(damageable => damageable != null)
                .Subscribe(damageable =>
                {
                    damageable.ApplyDamage();
                });
        }
    }
}
