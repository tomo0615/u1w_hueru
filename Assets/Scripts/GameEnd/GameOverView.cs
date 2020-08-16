using TMPro;
using UnityEngine;

namespace GameEnd
{
    public class GameOverView : MonoBehaviour
    {
        private TextMeshProUGUI _gameClearText;

        [SerializeField] private GameObject overPanel;
        
        public void InitializeGameEndViewer()
        {
            _gameClearText = GetComponent<TextMeshProUGUI>();
        }

        public void ViewGameOver()
        {
            _gameClearText.enabled = true;
            
            overPanel.SetActive(true);
        }
    }
}
