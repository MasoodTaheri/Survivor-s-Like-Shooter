public class PlayingState : IGameState
{
    private GameManager.GameManager _gameManager;
    public PlayingState(GameManager.GameManager gameManager)
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
        if (!_gameManager.PlayerController.isPlayerAlive())
            _gameManager.SetState(_gameManager._gameOverState);
    }
}
