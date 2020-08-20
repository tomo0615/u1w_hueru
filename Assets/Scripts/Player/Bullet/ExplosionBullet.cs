using Interfaces;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace Player.Bullet
{
    public class ExplosionBullet : MonoBehaviour
    {
        private Rigidbody2D _rigidbody2D;

        [SerializeField] private ExplosionCollision explosionCollision = default;

        private float _currentChargePower = 0.0f;
        public void InitializeBullet(Vector2 shotDirection, float chargePower)
        {
            this.OnCollisionEnter2DAsObservable()
                .Subscribe(_ =>
                {
                    _currentChargePower = chargePower;
                    InstanceExplosion();
                });
            
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _rigidbody2D.velocity = shotDirection;
        }

        private void InstanceExplosion()
        {
            var explosion 
                = Instantiate(explosionCollision, transform.position, Quaternion.identity);
            
            explosion.Explosion(_currentChargePower);
            
            Destroy(gameObject);
        }
    }
}
