using System.Collections.Generic;
using UnityEngine;

public class EffectHandler : MonoBehaviour
{
    [SerializeField] private Health Health;
    private List<BaseEffect> _effects;

    public void AddEffect(BaseEffect effect)
    {
        _effects.Add(effect);
    }

    public void RemoveEffect(BaseEffect effect)
    {
        _effects.Remove(effect);
    }

    public void Update()
    {
        foreach (var effect in _effects)
        {
            effect.OnUpdate();
        }
    }
}
