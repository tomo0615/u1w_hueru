using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Score
{
    public class ScoreView : MonoBehaviour
    {
        [SerializeField]
        private float punchTime = 0.5f;

        [SerializeField]
        private float punchScale = 1.5f;

        private bool _isPunch = false;

        private RectTransform _rectTransform;

        private TextMeshProUGUI _scoreText;

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();

            _scoreText = GetComponent<TextMeshProUGUI>();
        }

        private void Start()
        {
            _scoreText.text = "";
        }

        public void ViewScoreText(int value)
        {
            _scoreText.text = "Score:"+ value;

            //PunchText();
        }

        // private void PunchText()
        // {
        //     //重複して呼ばれないように
        //     if (_isPunch) return;
        //     _isPunch = true;
        //
        //     transform.DOScale(_rectTransform.localScale * punchScale, punchTime / 2)
        //         .OnComplete(() =>
        //         {
        //             transform.DOScale(_rectTransform.localScale / punchScale, punchTime / 2)
        //                 .OnComplete(() =>
        //                 {
        //                     _isPunch = false;
        //                 });
        //         });
        // }
    }
}
