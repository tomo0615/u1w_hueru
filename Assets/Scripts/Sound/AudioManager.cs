using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Sound
{
    public class AudioManager : MonoBehaviour
    {
        [SerializeField]
        private AudioSource bgmAudioSource = default;

        [SerializeField]
        private AudioSource seAudioSource = default;

        private Dictionary<BGMType, AudioClip> _bgmList;

        private Dictionary<SEType, AudioClip> _seList;

        [Inject]
        public void Construct(AudioTable audioTable)
        {
            _bgmList = new Dictionary<BGMType, AudioClip>
            {
                {BGMType.GameBGM, audioTable.gameBGM },
            };

            _seList = new Dictionary<SEType, AudioClip>
            {
                {SEType.ButtonOK, audioTable.buttonOk},

                {SEType.PlayerShot, audioTable.playerShot},
                {SEType.PlayerDamage, audioTable.playerDamage},
                {SEType.EnemyDawn, audioTable.enemyDawn},
                
                {SEType.ScoreGet, audioTable.scoreGet},
                
                {SEType.Explosion, audioTable.explosion},
            };
        }

        public void PlayBGM(BGMType type)
        {
            bgmAudioSource.clip = _bgmList[type];
            bgmAudioSource.Play();
        }

        public void PlaySE(SEType type)
        {
            seAudioSource.PlayOneShot(_seList[type]);
        }
    }
}
