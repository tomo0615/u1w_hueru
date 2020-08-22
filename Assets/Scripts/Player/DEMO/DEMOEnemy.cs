using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Interfaces;
using Sound;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using Zenject;

namespace Player.DEMO
{
    public class DEMOEnemy : MonoBehaviour,IDamageable
    {
        [SerializeField] private DEMOPlayer demoPlayer;
            
        [Inject]private AudioManager _audioManager;
        
        [SerializeField] private int hitPoint = 1;
        
        [SerializeField] private float dawnCoolTime = 3.0f;

        [SerializeField] private float vacuumableRange = 3.0f;
        
        [SerializeField] private Sprite defaultSprite = default;
        [SerializeField] private Sprite dawnSprite = default;

        private SpriteRenderer _spriteRenderer;
        
        private bool _isDawn = false;

        private Rigidbody2D _rigidbody2D;

        [SerializeField] private Transform originTransform;
        
        protected void Start()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _rigidbody2D = GetComponent<Rigidbody2D>();
            
            this.OnCollisionEnter2DAsObservable()
                .Where(_ => IsVacuumable() && demoPlayer.IsVacuumEnemy)
                .Subscribe(_ =>
                {
                    _audioManager.PlaySE(SEType.ScoreGet);
                    
                    _rigidbody2D.velocity = Vector2.zero;
                    
                    transform.position = originTransform.position;
                    _isDawn = false;
                    _spriteRenderer.sprite = defaultSprite;
                    hitPoint++;
                });
             
            this.UpdateAsObservable()
                .Where(_ => IsVacuumable() && demoPlayer.IsVacuumEnemy)
                .Subscribe(_ =>
                {
                    VaccuumedPlayer();
                });
        }
        
        private void VaccuumedPlayer()
        {
            var direction = (demoPlayer.transform.position - transform.position).normalized;
            _rigidbody2D.AddForce(direction * 1000f);
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
            
            _isDawn = true;
            
            DawnCoolTimeAsync(this.GetCancellationTokenOnDestroy()).Forget();
        }

        private async UniTaskVoid DawnCoolTimeAsync(CancellationToken token)
        {
            _spriteRenderer.sprite = dawnSprite;
            
            await UniTask.Delay(TimeSpan.FromSeconds(dawnCoolTime), cancellationToken: token);
            
            _spriteRenderer.sprite = defaultSprite;
            
            hitPoint++;
            
            _isDawn = false;
        }

        private bool IsVacuumable()
        {
            var sqrMagnitude = Vector3.SqrMagnitude(demoPlayer.transform.position - transform.position);

            return _isDawn && sqrMagnitude <= Mathf.Pow(vacuumableRange, 2);
        }
    }
}
