using TMPro;
using UnityEngine;

namespace Timer
{
    public class TimeView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI timeValueText = default;
        
        public void SetTimeValue(int timeValue)
        {
            timeValueText.text = timeValue.ToString();
        }
    }
}
