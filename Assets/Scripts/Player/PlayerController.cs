using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using EffectManager;
using GameEnd;
using Player.GUI.Life;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using Zenject;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        private PlayerInput _playerInput;

        private PlayerMover _playerMover;

        private PlayerAttacker _playerAttacker;

        private PlayerRotater _playerRotater;

        private GameEndPresenter _gameEndPresenter;

        private GameEffectManager _gameEffectManager;

        [SerializeField] private float moveSpeed = 10.0f;

        [SerializeField] private LifePresenter lifePresenter;
        
        private int _lifePoint = 3;

        [SerializeField] private float invincibleTime = 2.0f;
        public bool IsVacuumEnemy { get; private set; } = false;

        private bool _isDamageable = true;

        private readonly Vector3 _moveLimitPosition = new Vector3(8.7f, 0, 0);

        [SerializeField] private Sprite cannonSprite = default;
        [SerializeField] private Sprite vacuumSprite = default;

        [SerializeField] private SpriteRenderer weaponSpriteRenderer;

        private bool _isUpdatableObservable = true;

        [SerializeField] private ParticleSystem vacuumEffect; //EffectManagerに対応させる
        
        [Inject]
        private void Construct(PlayerInput playerInput, PlayerMover playerMover, PlayerAttacker playerAttacker
        ,PlayerRotater playerRotater, GameEndPresenter gameEndPresenter,GameEffectManager gameEffectManager)
        {
            _playerInput = playerInput;

            _playerMover = playerMover;
            
            _playerAttacker = playerAttacker;

            _playerRotater = playerRotater;

            _gameEndPresenter = gameEndPresenter;

            _gameEffectManager = gameEffectManager;
        }
        
        public void Initialize()
        {
            //入力
            this.UpdateAsObservable()
                .Where(_ => _isUpdatableObservable)
                .Subscribe(_ =>
                {
                    _playerInput.InputKeys();  
                });
            
            //回転
            this.UpdateAsObservable()
                .Where(_ => _isUpdatableObservable)
                .Subscribe(_ =>
                {
                    _playerRotater.LookMousePosition(_playerInput.LookDirection());
                });
            
            //移動
            this.UpdateAsObservable()
                .Where(_ => _isUpdatableObservable)
                .Subscribe(_ =>
                {
                    _playerMover.Move(_playerInput.MoveDirection() * moveSpeed);
                });
            
            this.UpdateAsObservable()
                .Where(_ => _isUpdatableObservable)
                .Subscribe(_ =>
                {
                    transform.localPosition =
                        new Vector3
                        (
                            Mathf.Clamp(transform.position.x, -_moveLimitPosition.x, _moveLimitPosition.x),
                            Mathf.Clamp(transform.position.y, -4.8f, 3.25f),
                            0
                        );
                });
            
            //バキューム
            this.UpdateAsObservable()
                .Where(_ => _isUpdatableObservable)
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
                .Where(_ => _isUpdatableObservable)
                .Where(_ => _playerInput.IsCharge())
                .Subscribe(_ =>
                {
                    _playerAttacker.Charge();
                });
            
            //攻撃
            this.UpdateAsObservable()
                .Where(_ => _isUpdatableObservable)
                .Where(_ => _playerInput.IsShot())
                .Subscribe(_ =>
                {
                    _playerAttacker.ShotBullet();
                });

            this.UpdateAsObservable()
                .Where(_ => _isUpdatableObservable)
                .Where(_ => _isDamageable == false)
                .Skip(1)
                .ThrottleFirst(TimeSpan.FromSeconds(0.5f))
                .Subscribe(_ =>
                {
                    _gameEffectManager.OnGenelateEffect(transform.position, EffectType.PlayerDamage);
                });
        }

        public void StopUpdateObservable()
        {
            _isUpdatableObservable = false;
        }

        public void AttackedEnemy()
        {
            if (_isDamageable == false) return;
            //無敵時間を可視化する
            _isDamageable = false;
            InvincibleTimeAsync(this.GetCancellationTokenOnDestroy()).Forget();
            
            _lifePoint--;
            lifePresenter.OnChangeLife();

            if(_lifePoint > 0) return;
            
            _gameEndPresenter.OnGameEnd(false);
        }

        private async UniTaskVoid InvincibleTimeAsync(CancellationToken token)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(invincibleTime), cancellationToken: token);

            _isDamageable = true;
        }
    }
}
