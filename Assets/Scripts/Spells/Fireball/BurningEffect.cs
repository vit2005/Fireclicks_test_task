using UnityEngine;

public class BurningEffect : BaseEffect
{
    private readonly int _damagePerTick;
    private readonly float _duration;
    private readonly float _interval;

    private Health _health;
    private float _elapsed;
    private float _tickTimer;

    public BurningEffect(int damagePerTick, float duration, float interval)
    {
        _damagePerTick = damagePerTick;
        _duration = duration;
        _interval = interval;
    }

    public override void ApplyEffect(Health health)
    {
        _health = health;
        _elapsed = 0f;
        _tickTimer = 0f;
    }

    public override void OnUpdate()
    {
        _elapsed += Time.deltaTime;
        _tickTimer += Time.deltaTime;

        if (_tickTimer >= _interval)
        {
            _tickTimer -= _interval;
            _health.TakeDamage(_damagePerTick);
        }

        if (_elapsed >= _duration)
            IsExpired = true;
    }

    public override void RemoveEffect() { }
}
