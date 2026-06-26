using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Health health;
    [SerializeField] private EffectHandler effectHandler;
    [SerializeField] private EnemyMovement movement;

    public event Action<Enemy> OnDeath;
    public event Action<Enemy> OnReadyToReturn;

    public Health Health => health;
    public EffectHandler EffectHandler => effectHandler;
    public int IncomingDamage { get; private set; }

    private Vector3 _originalScale;

    public void Init(EnemyConfig config, Tower tower)
    {
        _originalScale = transform.localScale;
        health.Init(config);
        movement.Init(config, tower);
        health.OnDeath += HandleDeath;
    }

    public void RegisterIncoming(int damage) => IncomingDamage += damage;

    public void UnregisterIncoming(int damage) => IncomingDamage = Mathf.Max(0, IncomingDamage - damage);

    public void ResetState()
    {
        health.OnDeath -= HandleDeath;
        movement.Stop();
        transform.localScale = _originalScale;
        IncomingDamage = 0;
    }

    public void NotifyReadyToReturn()
    {
        OnReadyToReturn?.Invoke(this);
    }

    private void HandleDeath()
    {
        health.OnDeath -= HandleDeath;
        movement.Stop();
        OnDeath?.Invoke(this);
    }
}
