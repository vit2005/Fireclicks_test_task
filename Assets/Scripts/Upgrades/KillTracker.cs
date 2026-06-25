using System;
using UnityEngine;

public class KillTracker : MonoBehaviour
{
    [SerializeField] private EnemySpawner enemySpawner;

    private static readonly int[] Milestones = { 10, 25, 50, 100, 175, 275, 400, 550, 750, 1000 };

    private int _killCount;
    private int _milestoneIndex;

    public event Action OnMilestone;

    private void OnEnable()
    {
        _killCount = 0;
        _milestoneIndex = 0;
        enemySpawner.OnEnemyKilled += HandleKill;
    }

    private void OnDisable()
    {
        enemySpawner.OnEnemyKilled -= HandleKill;
    }

    private void HandleKill()
    {
        _killCount++;

        if (_milestoneIndex < Milestones.Length && _killCount >= Milestones[_milestoneIndex])
        {
            _milestoneIndex++;
            OnMilestone?.Invoke();
        }
    }
}
