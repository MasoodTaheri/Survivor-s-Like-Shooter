using UnityEngine;

public class GameOverState : IGameState
{
    private readonly GameManager.GameManager _gameManager;

    public GameOverState(GameManager.GameManager gameManager)
    {
        _gameManager = gameManager;
    }

    public void Enter()
    {
        _gameManager._enemyController.DestroyAllEnemies();
        _gameManager.UIHandler.PlayerDeadPanelShow();
    }

    public void Update()
    {

    }

    public void Exit()
    {
        Debug.Log("Exiting Game Over State");
    }
}