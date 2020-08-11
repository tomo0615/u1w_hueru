using UnityEngine;
using Zenject;

namespace Player
{
    public class PlayerInstaller : MonoInstaller
    {
        [SerializeField] private PlayerAttacker playerAttacker;
        
        public override void InstallBindings()
        {
            Container
                .Bind<PlayerInput>()
                .AsCached();

            Container
                .Bind<PlayerMover>()
                .AsCached();

            Container
                .Bind<Rigidbody2D>()
                .FromComponentOnRoot()
                .AsCached();
            
            Container
                .BindInstance(transform)
                .AsCached();

            Container
                .BindInstance(playerAttacker)
                .AsCached();

            Container
                .Bind<PlayerRotater>()
                .AsCached();

            Container
                .BindInstance(Camera.main)
                .AsCached();
        }
    }
}
