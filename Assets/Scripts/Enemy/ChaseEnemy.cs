using UniRx;
using UniRx.Triggers;

namespace Enemy
{
    public class ChaseEnemy : BaseEnemy
    {
        private void Start()
        {
            Initialize();
            
            this.UpdateAsObservable()
                .Where(_=> IsVacuumable() == false)
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
