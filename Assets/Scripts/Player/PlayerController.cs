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
        
        [SerializeField] private float moveSpeed = 10f;
        
        [Inject]
        private void Construct(PlayerInput playerInput, PlayerMover playerMover, PlayerAttacker playerAttacker)
        {
            _playerInput = playerInput;

            _playerMover = playerMover;
            
            _playerAttacker = playerAttacker;
        }
        
        private void Start()
        {
            //入力
            this.UpdateAsObservable()
                .Subscribe(_ =>
                {
                    _playerInput.InputKeys();  
                });
            
            //移動
            this.UpdateAsObservable()
                .Subscribe(_ =>
                {
                    _playerMover.Move(_playerInput.MoveDirection() * moveSpeed);
                });
            
            //攻撃
            this.UpdateAsObservable()
                .Subscribe(_ =>
                {
                    
                });

        }
    }
}
