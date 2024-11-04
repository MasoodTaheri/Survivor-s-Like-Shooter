using Enemy;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace GameManager
{
    public class GameManager : MonoBehaviour
    {
        private IGameState _currentState;
        private IGameState _playingState;
        public IGameState _gameOverState;

        public PlayerController PlayerController;
        public PlayerData playerDataSO;

        [Inject][HideInInspector] public UIHandler UIHandler;
        [Inject][HideInInspector] public EnemiesController _enemyController;

        [Inject][HideInInspector] public LootManager LootManager;

        void Start()
        {
            _playingState = new PlayingState(this);
            _gameOverState = new GameOverState(this);


            var playerModel = new PlayerModel(playerDataSO);
            PlayerController.Init(playerModel, _enemyController);
            UIHandler.Initialize(playerModel, _enemyController);
            LootManager.Initialize(playerModel, PlayerController.gameObject, _enemyController);
            _enemyController.Initialize(PlayerController.gameObject);

            SetState(_playingState);
        }

        void Update()
        {
            _currentState?.Update();
        }

        public void SetState(IGameState newState)
        {
            _currentState?.Exit();
            _currentState = newState;
            _currentState.Enter();
        }
    }
}
