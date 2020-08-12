using Player;
using UniRx;
using UniRx.Triggers;
using UnityEngine.AI;
using Zenject;

namespace Enemy
{
    public class EscapeEnemy : BaseEnemy
    {
        private void Start()
        {
            Initialize();
            
            this.UpdateAsObservable()
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
