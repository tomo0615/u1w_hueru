using Player.Bullet;
using UnityEngine;

namespace Player.DEMO
{
    public class DEMOBullet : BaseBullet
    {
        [SerializeField] private DEMOExplosion explosionCollision = default;
        
        protected override void InstanceExplosion()
        {
            var explosion
                = Instantiate(explosionCollision, transform.position, Quaternion.identity);

            explosion.Explosion(CurrentChargePower);

            Destroy(gameObject);
        }
    }
}
