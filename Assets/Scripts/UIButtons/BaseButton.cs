using DG.Tweening;
using Sound;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UIButtons
{
    public class BaseButton : MonoBehaviour
    {
        [Inject] private AudioManager _audioManager = default;

        private Button _button;

        private const float ButtonAnimationTime = 0.1f;
        private const float ScaleUpValue = 1.1f;

        protected Button Button
        {
            get
            {
                if (_button == null)
                {
                    _button = GetComponent<Button>();
                }

                return _button;
            }
        }

        private RectTransform _rectTransform;

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
        }

        private void Start()
        {
            Button.OnClickAsObservable()
                .Subscribe(_ => { OnClicked(); })
                .AddTo(gameObject);
        }

        public virtual void OnClicked()
        {
            PlayClickedSound();

            DoPunchAnimation();
        }

        private void PlayClickedSound()
        {
            _audioManager.PlaySE(SEType.ButtonOK);
        }

        private void DoPunchAnimation()
        {
            DOTween.Sequence()
                .Append(_rectTransform.DOScale(Vector3.one * ScaleUpValue, ButtonAnimationTime))
                .Append(_rectTransform.DOScale(Vector3.one, ButtonAnimationTime));
        }
    }
}
