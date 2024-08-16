using UnityEngine;

public class GameOverState : IGameState
{
    private readonly GameManager _gameManager;

    public GameOverState(GameManager gameManager)
    {
        _gameManager = gameManager;
    }

    public void Enter()
    {
        Debug.Log("Entering Game Over State");
        _gameManager._enemyController.DestroyAllEnemies();
        _gameManager.UIHandler.PlayerDeadPanelShow();
    }

    public void Update()
    {

    }

    public void Exit()
    {
        // Cleanup before exiting the game over state
        Debug.Log("Exiting Game Over State");
    }
}