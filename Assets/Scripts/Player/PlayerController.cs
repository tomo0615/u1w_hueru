using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using GameEnd;
using Player.GUI.Life;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using Zenject;

namespace Player
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class PlayerController : MonoBehaviour
    {
        private PlayerInput _playerInput;

        private PlayerMover _playerMover;

        private PlayerAttacker _playerAttacker;

        private PlayerRotater _playerRotater;

        private GameEndPresenter _gameEndPresenter;

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
        
        [Inject]
        private void Construct(PlayerInput playerInput, PlayerMover playerMover, PlayerAttacker playerAttacker
        ,PlayerRotater playerRotater, GameEndPresenter gameEndPresenter)
        {
            _playerInput = playerInput;

            _playerMover = playerMover;
            
            _playerAttacker = playerAttacker;

            _playerRotater = playerRotater;

            _gameEndPresenter = gameEndPresenter;
        }
        
        private void Start()
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
            this.UpdateAsObservable()
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
                .Subscribe(_ =>
                {
                    IsVacuumEnemy = _playerInput.IsVacuum();

                    if (IsVacuumEnemy)
                    {
                        weaponSpriteRenderer.sprite = vacuumSprite;
                    }
                    else
                    {
                        weaponSpriteRenderer.sprite = cannonSprite;
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
                });
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
