using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Player.GUI
{
    public class ChargeGaugeView : MonoBehaviour
    {
        [SerializeField] private PlayerAttacker playerAttacker = default;
        
        private Image _image;

        private const int ChargeMaxValue = 2;
        
        private void Start()
        {
            _image = GetComponent<Image>();

            playerAttacker.ChargeTimeSave
                .Subscribe(OnChangeChargeValue);
        }

        private void OnChangeChargeValue(float chargeValue)
        {
            _image.fillAmount = chargeValue / ChargeMaxValue;
        }
    }
}
