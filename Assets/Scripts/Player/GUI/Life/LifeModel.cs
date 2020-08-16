using UniRx;

namespace Player.GUI.Life
{
    public class LifeModel
    {
        private ReactiveProperty<int> _lifeValue;

        public IReadOnlyReactiveProperty<int> LifeValue => _lifeValue;

        public LifeModel(int lifeValue)
        {
            _lifeValue = new ReactiveProperty<int>(lifeValue);
        }
        
        public void UpdateLifeValue(int lifeValue)
        {
            _lifeValue.Value -= lifeValue;
        }
    }
}
