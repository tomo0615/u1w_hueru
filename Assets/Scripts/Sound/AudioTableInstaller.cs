using UnityEngine;
using Zenject;

namespace Sound
{
    [CreateAssetMenu(fileName = "AudioTableInstaller", menuName = "Installers/AudioTableInstaller")]
    public class AudioTableInstaller : ScriptableObjectInstaller<AudioTableInstaller>
    {
        [SerializeField]
        private AudioTable audioTable = default;

        public override void InstallBindings()
        {
            Container
                .BindInstance(audioTable)
                .AsCached();
        }
    }
}
