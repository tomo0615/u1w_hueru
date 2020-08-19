using UnityEngine;
using Zenject;

namespace Sound
{
    public class TitleSoundSequencer : MonoBehaviour
    {
        [Inject] private AudioManager _audioManager;
        
        private void Start()
        {
            _audioManager.PlayBGM(BGMType.GameBGM);
        }
    }
}
