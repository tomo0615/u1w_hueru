using Player.Bullet;
using UniRx;
using UnityEngine;

namespace Player
{
    public class PlayerAttacker : MonoBehaviour
    {
        [SerializeField] private ExplosionBullet explosionBullet = default;

        [SerializeField] private float shotSpeed = 10f;

        [SerializeField] private Transform shotTransform = default;

        [SerializeField] private float chargeTime = 2.0f;
         
        private ReactiveProperty<float> _chargeTimeSave = new ReactiveProperty<float>(0.0f);

        public IReadOnlyReactiveProperty<float> ChargeTimeSave => _chargeTimeSave;

        private bool _isShotable = false;

        public void VacuumEnemy()
        {
        }

        public void Charge()
        { 
            _chargeTimeSave.Value += Time.deltaTime;
            
            if (_chargeTimeSave.Value < chargeTime) return;
            
            _isShotable = true;
        }

        public void ShotBullet()
        {
            if (_isShotable == false)
            {
                _chargeTimeSave.Value = 0.0f;
                _isShotable = false;
                return;
            }

            var bullet = Instantiate(explosionBullet, shotTransform.position, Quaternion.identity);
            
            bullet.InitializeBullet(transform.up * shotSpeed);

            _chargeTimeSave.Value = 0.0f;
            _isShotable = false;
        }
    }
}
