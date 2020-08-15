using TMPro;
using UnityEngine;

namespace GameEnd
{
    public class GameEndPresenter : MonoBehaviour
    {
        [SerializeField] private GameClearView gameClearView = default;

        [SerializeField] private GameOverView gameOverView;
        
        public bool IsGameEnd { private set; get; } = false;

        private bool IsClear { set; get; } = false;
        
        private void Awake()
        {
            gameClearView.InitializeGameEndViewer();
            
            gameOverView.InitializeGameEndViewer();
        }

        public void OnGameEnd(bool isClear)
        {
            IsGameEnd = true;

            IsClear = isClear;
        }

        public void ViewGameEnd()
        {
            if (IsClear)
            {
                gameClearView.ViewGameClear();
                return;
            }
            
            gameOverView.ViewGameOver(); 
        }
    }
}
