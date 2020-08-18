using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Player.GUI.Start
{
    public class StartView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI startText = default;
        
        public bool IsFinishedStartSignal { get; private set; }= false; 
        
        public void ViewStartSignal()
        {
            DOTween.Sequence()
                .OnStart(() => startText.text = "ready")
                .Append(transform.DOScale(Vector3.one / 1.2f, 2.0f)
                    .OnComplete(() => startText.text = "go").SetRelative())
                .Append(transform.DOScale(Vector3.one * 1.5f, 0.1f).SetRelative())
                .OnComplete(() =>
                {
                    startText.text = "";
                    IsFinishedStartSignal = true;
                });
        }
    }
}
