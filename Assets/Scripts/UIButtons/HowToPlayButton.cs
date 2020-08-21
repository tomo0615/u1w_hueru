using UIPanels;
using UnityEngine;

namespace UIButtons
{
    public class HowToPlayButton : BaseButton
    {
        [SerializeField] private HowToPlayPanel howToPlayPanel = default;

        [SerializeField] private GameObject titleCanvas;
        public override void OnClicked()
        {
            base.OnClicked();
            
            titleCanvas.SetActive(false);
            howToPlayPanel.ViewPanel();
        }
    }
}
