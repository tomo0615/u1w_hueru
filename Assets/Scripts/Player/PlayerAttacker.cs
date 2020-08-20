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
        
        public void Charge()
        {
            if (_chargeTimeSave.Value < chargeTime)
            { 
                _chargeTimeSave.Value += Time.deltaTime;
            }
        }

        public void ShotBullet()
        {
            var bullet = Instantiate(explosionBullet, shotTransform.position, Quaternion.identity);
            
            bullet.InitializeBullet(transform.up * shotSpeed, _chargeTimeSave.Value);
            
            _chargeTimeSave.Value = 0.0f;
        }
    }
}
