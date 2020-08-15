using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Interfaces;
using Player;
using Score;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace Enemy
{
    public abstract class BaseEnemy : MonoBehaviour, IDamageable
    {
        protected PlayerController PlayerController;

        private ScorePresenter _scorePresenter;
        
        [SerializeField] private int hitPoint = 1;
        
        [SerializeField] protected NavMeshAgent navMeshAgent = default;

        [SerializeField] private float dawnCoolTime = 3.0f;

        [SerializeField] private float vacuumableRange = 3.0f;

        [SerializeField] private int scoreValue = 10;
        
        [Inject]
        private void Construct(PlayerController playerController, ScorePresenter scorePresenter)
        {
            PlayerController = playerController;

            _scorePresenter = scorePresenter;
        }
        
         protected void Initialize()
         {
             this.OnCollisionEnter2DAsObservable()
                 .Where(other => 
                     other.gameObject == PlayerController.gameObject &&
                     IsVacuumable() == false)
                 .Subscribe(_ =>
                 {
                     PlayerController.AttackedEnemy();
                 });

             this.OnCollisionEnter2DAsObservable()
                 .Where(_ => IsVacuumable() && PlayerController.IsVacuumEnemy)
                 .Subscribe(_ =>
                 {
                     //TODO：Effect 
                     _scorePresenter.OnChangeScore(scoreValue);
                     
                     Destroy(gameObject);
                 });

             this.UpdateAsObservable()
                 .Where(_ => IsVacuumable() && PlayerController.IsVacuumEnemy)
                 .Subscribe(_ =>
                 {
                     VaccuumedPlayer();
                 });
         }

         private void VaccuumedPlayer()
         {
             transform.DOMove(PlayerController.transform.position, 0.5f);
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
            //TODO:点滅Animation
            navMeshAgent.isStopped = true;

            DawnCoolTimeAsync(this.GetCancellationTokenOnDestroy()).Forget();
        }

        private async UniTaskVoid DawnCoolTimeAsync(CancellationToken token)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(dawnCoolTime), cancellationToken: token);
            
            navMeshAgent.isStopped = false;
            hitPoint++;
        }

        protected bool IsVacuumable()
        {
            var sqrDistance = Vector3.SqrMagnitude(PlayerController.transform.position - transform.position);

            return navMeshAgent.isStopped &&
                   sqrDistance <= Mathf.Pow(vacuumableRange, 2);
        }
    }
}
