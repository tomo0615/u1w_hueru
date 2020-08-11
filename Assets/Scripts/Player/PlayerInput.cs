using UnityEngine;

namespace Player
{
    public class PlayerInput
    {
        private const KeyCode
            ShotKey = KeyCode.Space;
        
        private float _horizontal;
        private float _vertical;

        public void InputKeys()
        {
            _horizontal = Input.GetAxisRaw("Horizontal");
            _vertical = Input.GetAxisRaw("Vertical");
        }

        public Vector2 MoveDirection() =>
            new Vector2(_horizontal , _vertical).normalized;

        public Vector3 LookDirection() =>
            (Input.mousePosition);
        
        public bool IsShot() => Input.GetKeyUp(ShotKey);
    }
}
