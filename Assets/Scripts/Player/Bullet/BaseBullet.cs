using UnityEngine;
using UniRx;
using UniRx.Triggers;

namespace Player.Bullet
{
    public abstract class BaseBullet : MonoBehaviour
    {
        private Rigidbody2D _rigidbody2D;

        protected float CurrentChargePower = 0.0f;
        
        public void InitializeBullet(Vector2 shotDirection, float chargePower)
        {
            this.OnCollisionEnter2DAsObservable()
                .Subscribe(_ =>
                {
                    CurrentChargePower = chargePower;
                    InstanceExplosion();
                });
            
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _rigidbody2D.velocity = shotDirection;
        }

        protected abstract void InstanceExplosion();
    }
}
