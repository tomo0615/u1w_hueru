using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Enemy.Spawner;
using Interfaces;
using Player;
using Score;
using Sound;
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

        private EnemySpawner _enemySpawner;

        private AudioManager _audioManager;
        
        [SerializeField] private int hitPoint = 1;
        
        [SerializeField] protected NavMeshAgent navMeshAgent = default;

        [SerializeField] private float dawnCoolTime = 3.0f;

        [SerializeField] private float vacuumableRange = 3.0f;

        [SerializeField] private int scoreValue = 10;

        [SerializeField] private Sprite defaultSprite = default;
        [SerializeField] private Sprite dawnSprite = default;

        private SpriteRenderer _spriteRenderer;
        
        private bool _isDawn = false;

        [SerializeField] private float defaultSpeed = 3.5f;
        [SerializeField] private float dawnedSpeed = 10.0f;
        
        [Inject]
        private void Construct(PlayerController playerController, ScorePresenter scorePresenter
            , EnemySpawner enemySpawner ,AudioManager audioManager)
        {
            PlayerController = playerController;

            _scorePresenter = scorePresenter;

            _enemySpawner = enemySpawner;

            _audioManager = audioManager;
        }
        
         protected void Initialize()
         {
             _spriteRenderer = GetComponent<SpriteRenderer>();
             
             CollisionDisableAsync(this.GetCancellationTokenOnDestroy()).Forget();

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
                     _scorePresenter.OnChangeScore(scoreValue);
                     
                     _audioManager.PlaySE(SEType.ScoreGet);
                     
                     _enemySpawner.DecreaseEnemy();
                     Destroy(transform.root.gameObject);//子にクラスを持たせてるため
                 });
             
             this.UpdateAsObservable()
                 .Where(_ => IsVacuumable() && PlayerController.IsVacuumEnemy)
                 .Subscribe(_ =>
                 {
                     VaccuumedPlayer();
                 });
         }

         private async UniTaskVoid CollisionDisableAsync(CancellationToken token)
         {
             var collider = GetComponent<Collider2D>();
             
             collider.enabled = false;
             
             await UniTask.Delay(TimeSpan.FromSeconds(0.1f), cancellationToken: token);

             collider.enabled = true;
         } 

         private void VaccuumedPlayer()
         {
             navMeshAgent.isStopped = false;
             navMeshAgent.destination = PlayerController.transform.position;
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
            if(_isDawn) return;
            
            _audioManager.PlaySE(SEType.EnemyDawn);
            
            navMeshAgent.isStopped = true;
            _isDawn = true;
            
            DawnCoolTimeAsync(this.GetCancellationTokenOnDestroy()).Forget();
        }

        private async UniTaskVoid DawnCoolTimeAsync(CancellationToken token)
        {
            navMeshAgent.speed = dawnedSpeed;
            _spriteRenderer.sprite = dawnSprite;
            
            await UniTask.Delay(TimeSpan.FromSeconds(dawnCoolTime), cancellationToken: token);

            navMeshAgent.speed = defaultSpeed;
            _spriteRenderer.sprite = defaultSprite;

            navMeshAgent.isStopped = false;

            hitPoint++;
            
            _isDawn = false;
        }

        protected bool IsVacuumable()
        {
            var sqrMagnitude = Vector3.SqrMagnitude(PlayerController.transform.position - transform.position);

            return _isDawn && sqrMagnitude <= Mathf.Pow(vacuumableRange, 2);
        }
    }
}
