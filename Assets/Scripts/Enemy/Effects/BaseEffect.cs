using UnityEngine;

public abstract class BaseEffect
{
    public abstract void ApplyEffect(Health health);
    public abstract void OnUpdate();
    public abstract void RemoveEffect();

}
