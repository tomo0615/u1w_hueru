﻿using GameEnd;
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

        [SerializeField] private float moveSpeed = 10f;

        [SerializeField] private int lifePoint = 3;

        public bool IsVacuumEnemy { get; private set; } = false;
        
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
            lifePoint--;
            //無敵時間
            
            if(lifePoint > 0) return;
            
            Debug.Log("GameOver");
            _gameEndPresenter.OnGameEnd(false);
        }
    }
}
