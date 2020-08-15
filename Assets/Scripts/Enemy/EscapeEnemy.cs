using UniRx;
using UniRx.Triggers;

namespace Enemy
{
    public class EscapeEnemy : BaseEnemy
    {
        private void Start()
        {
            Initialize();
            
            this.UpdateAsObservable()
                .Where(_=> IsVacuumable() == false)
                .Subscribe(_=>
                {
                    EscapePlayer();
                });
        }

        private void EscapePlayer()
        {
            //TODO:精度をあげる
            navMeshAgent.destination = transform.position - PlayerController.transform.position;
        }
        
    }
}
