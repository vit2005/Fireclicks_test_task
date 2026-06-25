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
        _app.KillTracker.OnMilestone += OnMilestoneReached;
        _app.EnemySpawner.StartSpawning();
        _app.SpellCaster.enabled = true;
    }

    public void Exit()
    {
        _app.Tower.Health.OnDeath -= OnTowerDeath;
        _app.KillTracker.OnMilestone -= OnMilestoneReached;
        _app.EnemySpawner.StopSpawning();
        _app.SpellCaster.enabled = false;
        Time.timeScale = 0f;
    }

    private void OnTowerDeath()
    {
        _app.EndGame(Time.time - _startTime);
    }

    private void OnMilestoneReached()
    {
        Time.timeScale = 0f;
        _app.UpgradeScreen.Show(_app.GetRandomUpgrades(3), OnUpgradeSelected);
    }

    private void OnUpgradeSelected(UpgradeConfig upgrade)
    {
        upgrade.Effect.Apply(_app.GameContext);
        Time.timeScale = 1f;
    }
}
