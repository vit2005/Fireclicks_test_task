using System.Collections.Generic;
using UnityEngine;

public class EffectHandler : MonoBehaviour
{
    [SerializeField] private Health health;

    private readonly List<BaseEffect> _effects = new();
    private readonly List<BaseEffect> _toRemove = new();

    public void AddEffect(BaseEffect effect)
    {
        effect.ApplyEffect(health);
        _effects.Add(effect);
    }

    public void RemoveEffect(BaseEffect effect)
    {
        _toRemove.Add(effect);
    }

    private void Update()
    {
        foreach (var effect in _effects)
        {
            effect.OnUpdate();
            if (effect.IsExpired)
                _toRemove.Add(effect);
        }

        foreach (var effect in _toRemove)
        {
            effect.RemoveEffect();
            _effects.Remove(effect);
        }
        _toRemove.Clear();
    }

    public void ClearAll()
    {
        foreach (var effect in _effects)
            effect.RemoveEffect();
        _effects.Clear();
        _toRemove.Clear();
    }
}
