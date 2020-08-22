using DG.Tweening;
using UnityEngine;

namespace UIPanels
{
    public class HowToPlayPanel : MonoBehaviour
    {
        [SerializeField] private Transform cameraTransform;

        [SerializeField] private float cameraMoveTime = 0.5f;

        [SerializeField] private GameObject demoObject;
        
        public void ViewPanel()
        {
            if (Camera.main != null)
            { 
                Camera.main.transform.DOMoveX(cameraTransform.position.x, cameraMoveTime)
                    .OnComplete(() => 
                    { 
                        gameObject.SetActive(true); 
                        demoObject.SetActive(true);
                    });
            }
        }
    }
}
