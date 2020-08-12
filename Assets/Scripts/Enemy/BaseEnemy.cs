using Interfaces;
using Player;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.AI;

namespace Enemy
{
    public abstract class BaseEnemy : MonoBehaviour, IDamageable
    {
        [SerializeField] private int hitPoint = 1;
        
         public virtual void Start()
         {
             this.OnCollisionEnter2DAsObservable()
                 .Select(player => player.gameObject.GetComponent<PlayerController>())
                 .Where(player => player != null)
                 .Subscribe(player =>
                 {
                     player.AttackedEnemy();
                 });
         }
        
        //Playerのたまに当たったら
        public void ApplyDamage()
        {
            hitPoint--;

            if (hitPoint > 0) return;
            
            Dawn();
        }

        private void Dawn()
        {
            //移動状態になり、吸い込み可能になる
        }
    }
}
