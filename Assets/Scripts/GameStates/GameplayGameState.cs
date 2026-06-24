using UnityEngine;

public class GameplayGameState : IGameState
{
    private readonly AppController _app;
    private float _startTime;

    public GameplayGameState(AppController app) => _app = app;

    public void Enter()
    {
        _startTime = Time.time;
        Time.timeScale = 1f;
        _app.Tower.Health.OnDeath += OnTowerDeath;
    }

    public void Exit()
    {
        _app.Tower.Health.OnDeath -= OnTowerDeath;
        Time.timeScale = 0f;
    }

    private void OnTowerDeath()
    {
        _app.EndGame(Time.time - _startTime);
    }
}
