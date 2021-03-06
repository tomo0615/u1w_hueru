﻿using UnityEngine;

namespace Player.Bullet
{
    public class ExplosionBullet : BaseBullet
    {
        [SerializeField] private ExplosionCollision explosionCollision = default;
        
        protected override void InstanceExplosion()
        {
            var explosion 
                = Instantiate(explosionCollision, transform.position, Quaternion.identity);
            
            explosion.Explosion(CurrentChargePower);
            
            Destroy(gameObject);
        }
    }
}
