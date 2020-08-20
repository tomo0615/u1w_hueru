
using UnityEngine;

namespace Sound
{
    [CreateAssetMenu(menuName = "DataTable/AudioTable", fileName = "AudioTable")]
    public class AudioTable : ScriptableObject
    {
        #region BGM
        public AudioClip gameBGM;
        #endregion

        #region Common Audio
        public AudioClip buttonOk;
        #endregion

        #region GameAudio
        public AudioClip playerShot;

        public AudioClip playerDamage;

        public AudioClip enemyDawn;

        public AudioClip scoreGet;

        public AudioClip explosion;

        public AudioClip charge;

        public AudioClip chargeEnd;
        #endregion
    }
}