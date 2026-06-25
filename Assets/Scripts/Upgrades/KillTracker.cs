using System;
using UnityEngine;

public class KillTracker : MonoBehaviour
{
    [SerializeField] private EnemySpawner enemySpawner;
    [SerializeField] private int killsPerUpgrade = 10;

    private int _killCount;

    public event Action OnMilestone;

    private void OnEnable()
    {
        _killCount = 0;
        enemySpawner.OnEnemyKilled += HandleKill;
    }

    private void OnDisable()
    {
        enemySpawner.OnEnemyKilled -= HandleKill;
    }

    private void HandleKill()
    {
        _killCount++;

        if (_killCount % killsPerUpgrade == 0)
            OnMilestone?.Invoke();
    }
}
