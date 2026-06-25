using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Health health;
    [SerializeField] private EffectHandler effectHandler;
    [SerializeField] private EnemyMovement movement;

    public event Action<Enemy> OnDeath;
    public Health Health => health;
    public EffectHandler EffectHandler => effectHandler;

    public void Init(EnemyConfig config, Tower tower)
    {
        health.Init(config);
        movement.Init(config, tower);
        transform.localScale = Vector3.one * config.Scale;
        health.OnDeath += HandleDeath;
    }

    public void ResetState()
    {
        health.OnDeath -= HandleDeath;
        movement.Stop();
    }

    private void HandleDeath()
    {
        health.OnDeath -= HandleDeath;
        movement.Stop();
        OnDeath?.Invoke(this);
    }
}
