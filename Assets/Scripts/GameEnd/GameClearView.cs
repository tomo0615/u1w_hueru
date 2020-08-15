using TMPro;
using UnityEngine;

namespace GameEnd
{
    public class GameClearView : MonoBehaviour
    {
        private TextMeshProUGUI _gameClearText;

        public void InitializeGameEndViewer()
        {
            _gameClearText = GetComponent<TextMeshProUGUI>();
        }

        public void ViewGameClear()
        {
            _gameClearText.enabled = true;
        }
    }
}
