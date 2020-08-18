using Score;
using Zenject;

namespace UIButtons
{
    public class RankingButton : BaseButton
    {
        [Inject] private ScorePresenter _scorePresenter;
        
        public override void OnClicked()
        {
            base.OnClicked();

            var scoreValue = _scorePresenter.GetScoreValue();
            naichilab.RankingLoader.Instance.SendScoreAndShowRanking (scoreValue);
        }
    }
}
