using System;
using UniRx;

namespace Timer
{
    public class TimeModel
    {
        private ReactiveProperty<int> _gameTimer;

        public IReadOnlyReactiveProperty<int> GameTimer => _gameTimer;


        public TimeModel()
        {
            _gameTimer = new ReactiveProperty<int>(0);
        }
        
        public void SetTimerValue(int timeValue)
        {
            _gameTimer.Value = timeValue;
        }
        
        public IObservable<int> CreateTimerObservable(int timeValue)
        {
            _gameTimer = new ReactiveProperty<int>(timeValue);

            return Observable
                .Timer(TimeSpan.FromSeconds(0), TimeSpan.FromSeconds(1))
                .Select(x => (int)(timeValue - x))
                .TakeWhile(x => x >= 0);
        }
    }
}
