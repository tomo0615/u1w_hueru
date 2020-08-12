using Player;
using UniRx;
using UniRx.Triggers;
using UnityEngine.AI;
using Zenject;

namespace Enemy
{
    public class ChaseEnemy : BaseEnemy
    {
        [Inject] private PlayerController _playerController;

        private NavMeshAgent _navMeshAgent;
        
        public override void Start()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();

            this.UpdateAsObservable()
                .Subscribe(_=>
                {
                    ChasePlayer();
                });
        }

        private void ChasePlayer()
        {
            _navMeshAgent.destination = _playerController.transform.position;
        }
    }
}
