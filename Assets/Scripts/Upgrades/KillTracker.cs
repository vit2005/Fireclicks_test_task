using System;
using UnityEngine;

public class KillTracker : MonoBehaviour
{
    [SerializeField] private EnemySpawner enemySpawner;
    [SerializeField] private KillTrackerConfig config;

    private int _killCount;
    private int _nextMilestone;

    public int KillCount => _killCount;
    public int NextMilestone => _nextMilestone;

    public event Action OnMilestone;
    public event Action OnKill;

    public void Awake()
    {
        _killCount = 0;
        _nextMilestone = config.FirstMilestone;
    }

    private void OnEnable()
    {
        enemySpawner.OnEnemyKilled += HandleKill;
    }

    private void OnDisable()
    {
        enemySpawner.OnEnemyKilled -= HandleKill;
    }

    private void HandleKill()
    {
        _killCount++;
        OnKill?.Invoke();

        if (_killCount >= _nextMilestone)
        {
            _killCount = 0;
            _nextMilestone += config.MilestoneStep;
            OnMilestone?.Invoke();
        }
    }
}
