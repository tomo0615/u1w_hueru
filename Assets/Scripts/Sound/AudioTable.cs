
using UnityEngine;

namespace Sound
{
    [CreateAssetMenu(menuName = "DataTable/AudioTable", fileName = "AudioTable")]
    public class AudioTable : ScriptableObject
    {
        #region BGM
        public AudioClip titleBGM;

        public AudioClip gameBGM;

        public AudioClip resultBGM;
        #endregion

        #region Common Audio
        public AudioClip buttonOk;

        public AudioClip buttonCancel;
        #endregion

        #region GameAudio
        public AudioClip unitAttack;

        public AudioClip unitDamage;

        public AudioClip unitDeath;

        public AudioClip enemyAttack;

        public AudioClip enemyDamage;

        public AudioClip enemyDestroy;

        public AudioClip win;

        public AudioClip lose;
        #endregion
    }
}