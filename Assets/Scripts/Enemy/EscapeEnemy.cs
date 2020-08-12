using Player;
using UniRx;
using UniRx.Triggers;
using UnityEngine.AI;
using Zenject;

namespace Enemy
{
    public class EscapeEnemy : BaseEnemy
    {
        [Inject] private PlayerController _playerController;

        private NavMeshAgent _navMeshAgent;

        public override void Start()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();

            this.UpdateAsObservable()
                .Subscribe(_=>
                {
                    EscapePlayer();
                });
        }

        private void EscapePlayer()
        {
            _navMeshAgent.destination = transform.position - _playerController.transform.position;
        }
        
    }
}
