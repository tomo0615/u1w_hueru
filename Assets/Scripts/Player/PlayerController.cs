﻿using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using GameEnd;
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

        [SerializeField] private float moveSpeed = 10.0f;

        [SerializeField] private int lifePoint = 3;

        [SerializeField] private float invincibleTime = 2.0f;
        public bool IsVacuumEnemy { get; private set; } = false;

        private bool _isDamageable = true;
            
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
            
            //バキューム
            this.UpdateAsObservable()
                .Subscribe(_ =>
                {
                    IsVacuumEnemy = _playerInput.IsVacuum();
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
            
            lifePoint--;

            if(lifePoint > 0) return;
            
            _gameEndPresenter.OnGameEnd(false);
        }

        private async UniTaskVoid InvincibleTimeAsync(CancellationToken token)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(invincibleTime), cancellationToken: token);

            _isDamageable = true;
        }
    }
}
