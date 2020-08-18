using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Score
{
    public class ScoreView : MonoBehaviour
    {
        private TextMeshProUGUI _scoreText;

        private void Awake()
        {
            _scoreText = GetComponent<TextMeshProUGUI>();
        }

        private void Start()
        {
            _scoreText.text = "Score:" + 0.ToString("D5");
        }

        public void ViewScoreText(int value)
        {
            _scoreText.text = "Score:"+ value.ToString("D5");
        }
    }
}
