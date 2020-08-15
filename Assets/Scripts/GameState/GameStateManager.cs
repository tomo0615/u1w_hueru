using GameEnd;
using Player;
using UnityEngine;
using Zenject;

namespace GameState
{
    public class GameStateManager : StateMachine<GameState>
    { 
        private PlayerController _playerController;
        
        private GameEndPresenter _gameEndPresenter;
        
        [Inject]
        private void Construct(PlayerController playerController, GameEndPresenter gameEndPresenter)
        {
            _playerController = playerController;

            _gameEndPresenter = gameEndPresenter;
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
        
        #region SettingMethod
        private void OnSetUpSetting()
        {

        }

        private void OnUpdateSetting()
        {
            GoToState(GameState.Game);
        }
        #endregion

        #region GameMethod

        private void OnSetUpGame()
        {
 
        }

        private void OnUpdateGame()
        {
            //Playerを使えるようにする
            //playerController.UpdatePlayerAction();

            if (_gameEndPresenter.IsGameEnd)
            {
                GoToState(GameState.Finish);
            }
        }
        #endregion

        #region FinishMethod
        private void OnSetUpFinish()
        {
            _gameEndPresenter.ViewGameEnd();
        }
        private void OnUpdateFinish()
        {
            //Debug.Log("Finish");
        }
        #endregion
    }
}
