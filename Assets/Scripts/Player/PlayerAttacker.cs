using Player.Bullet;
using UnityEngine;

namespace Player
{
    public class PlayerAttacker : MonoBehaviour
    {
        [SerializeField] private ExplosionBullet explosionBullet;

        [SerializeField] private float shotSpeed = 10f;

        [SerializeField] private Transform shotTransform;

        [SerializeField] private float chargeTime = 2.0f;

        private float _chargeTimeSave = 0.0f;

        private bool _isShotable = false;
        public void VacuumEnemy()
        {
            
        }

        public void Charge()
        {
            _chargeTimeSave += Time.deltaTime;

            if (_chargeTimeSave >= chargeTime)
            {
                _isShotable = true;
            }
        }

        public void ShotBullet()
        {
            if (_isShotable == false)
            {
                _isShotable = false;
                return;
            }

            var bullet = Instantiate(explosionBullet, shotTransform.position, Quaternion.identity);
            
            bullet.SetShotDirection(transform.up * shotSpeed);

            _chargeTimeSave = 0.0f;
            _isShotable = false;
        }
    }
}
