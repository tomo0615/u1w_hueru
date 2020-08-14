using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Interfaces;
using Player;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace Enemy
{
    public abstract class BaseEnemy : MonoBehaviour, IDamageable
    {
        [Inject] protected PlayerController PlayerController;
        
        [SerializeField] private int hitPoint = 1;
        
        [SerializeField] protected NavMeshAgent navMeshAgent = default;

        [SerializeField] private float dawnCoolTime = 3.0f;
         protected void Initialize()
         {
             this.OnCollisionEnter2DAsObservable()
                 .Where(other => 
                     other.gameObject == PlayerController.gameObject &&
                     navMeshAgent.isStopped == false)
                 .Subscribe(_ =>
                 {
                     PlayerController.AttackedEnemy();
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
            //TODO:吸い込み可能になる 点滅Animation
            navMeshAgent.velocity = Vector3.zero;
            navMeshAgent.isStopped = true;

            DawnCoolTimeAsync(this.GetCancellationTokenOnDestroy()).Forget();
        }
        
        private async UniTaskVoid DawnCoolTimeAsync(CancellationToken token)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(dawnCoolTime), cancellationToken: token);
            
            navMeshAgent.isStopped = false;
            hitPoint++;
        }
    }
}
