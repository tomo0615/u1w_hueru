using Score;
using UnityEngine;
using Zenject;

namespace UIButtons
{
    public class TweetButton : BaseButton
    {
        [Inject] private ScorePresenter _scorePresenter;

        [SerializeField] private string gameId = default;
        
        public override void OnClicked()
        {
            base.OnClicked();

            var scoreValue = _scorePresenter.GetScoreValue();
            
            naichilab.UnityRoomTweet
                .Tweet(gameId,"Score:" + scoreValue + "を獲得しました！","unityRoom","unity1week");
        }
    }
}
