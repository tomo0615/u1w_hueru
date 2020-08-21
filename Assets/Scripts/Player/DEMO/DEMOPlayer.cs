using System;
using EffectManager;
using Sound;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using Zenject;

namespace Player.DEMO
{
    public class DEMOPlayer : MonoBehaviour
    {
        private PlayerInput _playerInput;

        private PlayerMover _playerMover;

        private PlayerAttacker _playerAttacker;

        private PlayerRotater _playerRotater;
        
        private GameEffectManager _gameEffectManager;

        private AudioManager _audioManager;
        
        [SerializeField] private float moveSpeed = 10.0f;
        
        public bool IsVacuumEnemy { get; private set; } = false;

        private bool _isDamageable = true;
        
        [SerializeField] private Sprite cannonSprite = default;
        [SerializeField] private Sprite vacuumSprite = default;

        [SerializeField] private SpriteRenderer weaponSpriteRenderer;
        
        [SerializeField] private ParticleSystem vacuumEffect; //EffectManagerに対応させる
        
        [Inject]
        private void Construct(PlayerInput playerInput, PlayerMover playerMover, PlayerAttacker playerAttacker
        ,PlayerRotater playerRotater,GameEffectManager gameEffectManager
        ,AudioManager audioManager)
        {
            _playerInput = playerInput;

            _playerMover = playerMover;
            
            _playerAttacker = playerAttacker;

            _playerRotater = playerRotater;
            
            _gameEffectManager = gameEffectManager;

            _audioManager = audioManager;
        }
        
        //DEMOちゅうのみにする
        public void Start()
        {
            //入力
            this.UpdateAsObservable()
                .Subscribe(_ =>
                {
                    _playerInput.InputKeys();  
                });
            
            //回転
            this.UpdateAsObservable()
                .Subscribe(_ =>
                {
                    _playerRotater.LookMousePosition(_playerInput.LookDirection());
                });
            
            //移動
            this.UpdateAsObservable()
                .Subscribe(_ =>
                {
                    _playerMover.Move(_playerInput.MoveDirection() * moveSpeed);
                });

            //バキューム
            this.UpdateAsObservable()
                .Subscribe(_ =>
                {
                    IsVacuumEnemy = _playerInput.IsVacuum();

                    if (IsVacuumEnemy)
                    {
                        weaponSpriteRenderer.sprite = vacuumSprite;
                        vacuumEffect.Play();
                    }
                    else
                    {
                        weaponSpriteRenderer.sprite = cannonSprite;
                        vacuumEffect.Stop();
                    }
                });
            
            //チャージ
            this.UpdateAsObservable()
                .Where(_ => _playerInput.IsCharge())
                .Subscribe(_ =>
                {
                    _playerAttacker.Charge();
                });
            
            //攻撃
            this.UpdateAsObservable()
                .Where(_ => _playerInput.IsShot())
                .Subscribe(_ =>
                {
                    _playerAttacker.ShotBullet();
                    
                    _audioManager.PlaySE(SEType.PlayerShot);
                });

            //VacuumEffect
            this.UpdateAsObservable()
                .Where(_ => _isDamageable == false)
                .ThrottleFirst(TimeSpan.FromSeconds(0.5f))
                .Subscribe(_ =>
                {
                    _gameEffectManager.OnGenelateEffect(transform.position, EffectType.PlayerDamage);
                });
        }
    }
}
