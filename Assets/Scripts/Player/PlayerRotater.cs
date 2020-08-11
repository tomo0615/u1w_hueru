using UnityEngine;

namespace Player
{
    public class PlayerRotater
    {
        private readonly Transform _transform;

        private readonly Camera _camera;
        
        public PlayerRotater(Transform transform, Camera camera)
        {
            _transform = transform;

            _camera = camera;
        }

        public void LookMousePosition(Vector3 mouseDirection)
        {
            var position = _camera.WorldToScreenPoint(_transform.localPosition);
            var rotation = Quaternion.LookRotation(Vector3.forward, mouseDirection - position);
            _transform.localRotation = rotation;
        }
    }
}
