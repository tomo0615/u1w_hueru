using Player;
using UniRx;
using UniRx.Triggers;
using Zenject;

namespace Enemy
{
    public class ChaseEnemy : BaseEnemy
    {
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
            navMeshAgent.destination = PlayerController.transform.position;
        }
    }
}
