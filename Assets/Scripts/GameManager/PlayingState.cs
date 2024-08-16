public class PlayingState : IGameState
{
    private GameManager _gameManager;
    public PlayingState(GameManager gameManager)
    {
        _gameManager = gameManager;
    }
    public void Enter()
    {
        _gameManager._enemyController.StartEnemySpawner();
    }

    public void Exit()
    {

    }

    public void Update()
    {
        if (!_gameManager._playerController.isPlayerAlive())
            _gameManager.SetState(_gameManager._gameOverState);
    }
}
