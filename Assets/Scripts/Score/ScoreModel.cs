using System;
using Cysharp.Threading.Tasks;
using UniRx;

namespace Score
{
    public class ScoreModel
    {
        private ReactiveProperty<int> _scoring;

        public IReadOnlyReactiveProperty<int> Scoring => _scoring;

        private float _comboValue = 0;

        private readonly float _comboContinueTime = 2.0f;
        
        public ScoreModel()
        {
            _scoring = new ReactiveProperty<int>(0);
        }

        public void UpdateScoreValue(int value)
        {
            var score = value * (1 + _comboValue);
            
            _scoring.Value += (int)score;
            
            _comboValue += 0.1f;
            ComboAsync().Forget();
        }

        private async UniTaskVoid ComboAsync()
        {
            await UniTask.Delay(TimeSpan.FromSeconds(_comboContinueTime));

            _comboValue = 0;
        }
    }
}
