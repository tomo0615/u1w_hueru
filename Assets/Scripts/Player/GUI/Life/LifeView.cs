using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Player.GUI.Life
{
    public class LifeView : MonoBehaviour
    {
        [SerializeField]private List<Image> lifeList = new List<Image>();

        private int _currentIndex = 0;
        
        public void OnDecreaseLife()
        {
            lifeList[_currentIndex].enabled = false;
            _currentIndex++;
        }
    }
}
