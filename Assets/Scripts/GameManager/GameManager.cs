using UnityEngine;
using Zenject;


//public enum state { Playing, Dead }
public class GameManager : MonoBehaviour
{
    public IGameState _currentState;
    public IGameState _playingState;
    public IGameState _gameOverState;

    public PlayerController _playerController;
    public PlayerData playerDataSO;

    [Inject][HideInInspector] public UIHandler UIHandler;
    [Inject][HideInInspector] public EnemiesController _enemyController;

    [Inject][HideInInspector] public LootManager LootManager;

    void Start()
    {
        _playingState = new PlayingState(this);
        _gameOverState = new GameOverState(this);


        var playerModel = new PlayerModel(playerDataSO);
        _playerController.Init(playerModel, _enemyController);
        UIHandler.Initialize(playerModel, _enemyController);
        LootManager.Initialize(playerModel, _playerController.gameObject, _enemyController);
        _enemyController.Initialize(_playerController.gameObject);

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
