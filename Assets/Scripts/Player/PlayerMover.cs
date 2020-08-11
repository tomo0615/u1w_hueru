using UnityEngine;

namespace Player
{
    public class PlayerMover
    {
        private readonly Rigidbody2D _rigidbody2D;

        private readonly Transform _transform;
        
        public PlayerMover(Rigidbody2D rigidbody2D, Transform transform)
        {
            _rigidbody2D = rigidbody2D;

            _transform = transform;
        }
        
        public void Move(Vector3 moveDirection)
        {
            _rigidbody2D.velocity =  moveDirection;
        }
    }
}
