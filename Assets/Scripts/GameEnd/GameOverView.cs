using TMPro;
using UnityEngine;

namespace GameEnd
{
    public class GameOverView : MonoBehaviour
    {
        private TextMeshProUGUI _gameClearText;

        public void InitializeGameEndViewer()
        {
            _gameClearText = GetComponent<TextMeshProUGUI>();
        }

        public void ViewGameOver()
        {
            _gameClearText.enabled = true;
        }
    }
}
