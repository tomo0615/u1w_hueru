using Enemy.Spawner;
using GameEnd;
using Player;
using Player.GUI.Start;
using SceneLoader;
using Timer;
using Zenject;

namespace GameState
{
    public class GameStateManager : StateMachine<GameState>
    { 
        private PlayerController _playerController;
        
        private GameEndPresenter _gameEndPresenter;

        private TimePresenter _timePresenter;

        private EnemySpawner _enemySpawner;

        private FadeSceneLoader _fadeSceneLoader;

        private StartView _startView;

        [Inject]
        private void Construct(PlayerController playerController, GameEndPresenter gameEndPresenter, TimePresenter timePresenter,
            EnemySpawner enemySpawner, FadeSceneLoader fadeSceneLoader, StartView startView)
        {
            _playerController = playerController;

            _gameEndPresenter = gameEndPresenter;

            _timePresenter = timePresenter;

            _enemySpawner = enemySpawner;

            _fadeSceneLoader = fadeSceneLoader;

            _startView = startView;
        }
        private void Awake()
        {
            InitializeStateMachine();
        }

        private void Start()
        {
            GoToState(GameState.Setting);
        }

        private void InitializeStateMachine()
        {
            //FadeOut
            {
                var state = new State<GameState>(GameState.FadeOut);
                state.UpdateAction = OnUpdateFadeOut;
                AddState(state);
            }
            //Setting
            {
                var state = new State<GameState>(GameState.Setting);
                state.SetUpAction = OnSetUpSetting;
                state.UpdateAction = OnUpdateSetting;
                AddState(state);
            }
            //Game
            {
                var state = new State<GameState>(GameState.Game);
                state.SetUpAction = OnSetUpGame;
                state.UpdateAction = OnUpdateGame;
                AddState(state);
            }
            //Finish
            {
                var state = new State<GameState>(GameState.Finish);
                state.SetUpAction = OnSetUpFinish;
                state.UpdateAction = OnUpdateFinish;
                AddState(state);
            }
        }

        #region  FadeOutMethod
        private void OnUpdateFadeOut()
        {
            if (_fadeSceneLoader.IsFadeOutCompleted == false) return;
            
            GoToState(GameState.Setting);
        }
        #endregion

        #region SettingMethod
        private void OnSetUpSetting()
        {
            _startView.ViewStartSignal();
        }

        private void OnUpdateSetting()
        {
            if (_startView.IsFinishedStartSignal == false) return;
            
            GoToState(GameState.Game);
        }
        #endregion

        #region GameMethod

        private void OnSetUpGame()
        {
            _playerController.Initialize();
            
            _enemySpawner.Initialize();
            
            _timePresenter.OnStartTimer(() =>
            {
                _gameEndPresenter.OnGameEnd(false);//GameOver
            });
        }

        private void OnUpdateGame()
        {
            if (_gameEndPresenter.IsGameEnd)
            {
                GoToState(GameState.Finish);
            }
        }
        #endregion

        #region FinishMethod
        private void OnSetUpFinish()
        {
            _playerController.StopUpdateObservable();
            
            _timePresenter.OnStopTimer();
            
            _gameEndPresenter.ViewGameEnd();
        }
        private void OnUpdateFinish()
        {
            //Debug.Log("Finish");
        }
        #endregion
    }
}
