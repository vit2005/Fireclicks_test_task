public abstract class BaseEffect
{
    public bool IsExpired { get; protected set; }

    public abstract void ApplyEffect(Health health);
    public abstract void OnUpdate();
    public abstract void RemoveEffect();
}
