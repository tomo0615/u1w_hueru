using UnityEngine;

namespace Player.Bullet
{
    public class ExplosionBullet : MonoBehaviour
    {
        private Rigidbody2D _rigidbody2D;

        public void SetShotDirection(Vector2 shotDirection)
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _rigidbody2D.velocity = shotDirection;
        }
    }
}
