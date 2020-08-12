using Player;
using UniRx;
using UniRx.Triggers;
using Zenject;

namespace Enemy
{
    public class ChaseEnemy : BaseEnemy
    {
        [Inject] private PlayerController _playerController;
        
        private void Start()
        {
            Initialize();

            this.UpdateAsObservable()
                .Subscribe(_=>
                {
                    ChasePlayer();
                });
        }

        private void ChasePlayer()
        {
            navMeshAgent.destination = _playerController.transform.position;
        }
    }
}
