using UniRx;
using UnityEngine;

namespace Player.GUI.Life
{
    public class LifePresenter : MonoBehaviour
    {
        [SerializeField] private LifeView lifeView;

        private LifeModel _lifeModel;
        
        private void Start()
        {            
            _lifeModel = new LifeModel(3);
            
            _lifeModel.LifeValue
                .Skip(1)
                .Subscribe(_ => lifeView.OnDecreaseLife());
        }

        public void OnChangeLife()
        {
            _lifeModel.UpdateLifeValue(1);
        }
    }
}
