using System;
using Player.Bullet;
using Sound;
using UniRx;
using UnityEngine;
using Zenject;

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

        [Inject] private AudioManager _audioManager;
        
        public void Charge()
        {
            if (_chargeTimeSave.Value <= 0.0f)
            {
                _audioManager.PlaySE(SEType.Charge);
            }
            
            if (_chargeTimeSave.Value >= chargeTime) return;
            
            _chargeTimeSave.Value += Time.deltaTime;

            if ((Math.Abs(_chargeTimeSave.Value - chargeTime) >= 0.1f)) return;
            
            _audioManager.StopSE();
            _audioManager.PlaySE(SEType.ChargeEnd);
        }

        public void ShotBullet()
        {
            _audioManager.StopSE();
            var bullet = Instantiate(explosionBullet, shotTransform.position, Quaternion.identity);
            
            bullet.InitializeBullet(transform.up * shotSpeed, _chargeTimeSave.Value);
            
            _chargeTimeSave.Value = 0.0f;
        }
    }
}
