using TMPro;
using UnityEngine;

namespace GameEnd
{
    public class GameClearView : MonoBehaviour
    {
        private TextMeshProUGUI _gameClearText;

        [SerializeField] private GameObject clearPanel;
        
        public void InitializeGameEndViewer()
        {
            _gameClearText = GetComponent<TextMeshProUGUI>();
        }

        public void ViewGameClear()
        {
            _gameClearText.enabled = true;
            
            clearPanel.SetActive(true);
        }
    }
}
