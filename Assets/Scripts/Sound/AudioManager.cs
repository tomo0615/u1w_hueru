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
                {BGMType.TitleBGM, audioTable.titleBGM},
                {BGMType.GameBGM, audioTable.gameBGM },
                {BGMType.ResultBGM, audioTable.resultBGM}
            };

            _seList = new Dictionary<SEType, AudioClip>
            {
                {SEType.ButtonOK, audioTable.buttonOk},
                {SEType.ButtonCancel, audioTable.buttonCancel},

                {SEType.UnitAttack, audioTable.unitAttack},
                {SEType.UnitDamage, audioTable.unitDamage},
                {SEType.UnitDeath, audioTable.unitDeath},

                {SEType.EnemyAttack, audioTable.enemyAttack},
                {SEType.EnemyDamage, audioTable.enemyDamage},
                {SEType.EnemyDestory, audioTable.enemyDestroy},

                {SEType.Win, audioTable.win},
                {SEType.Lose, audioTable.lose},
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
