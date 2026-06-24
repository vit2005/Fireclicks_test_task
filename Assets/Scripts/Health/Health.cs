using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    public event Action<int> OnDamaged;
    public event Action OnDeath;

    private int _maxHealth;
    private int _currentHealth;
    private bool _isDead;

    public int CurrentHealth => _currentHealth;
    public int MaxHealth => _maxHealth;

    public void Init(IHealthDataProvider data)
    {
        _maxHealth = data.maxHealth;
        _currentHealth = _maxHealth;
        _isDead = false;
    }

    public void TakeDamage(int amount)
    {
        if (_isDead || amount <= 0) return;

        _currentHealth = Mathf.Max(0, _currentHealth - amount);
        OnDamaged?.Invoke(amount);

        if (_currentHealth == 0)
        {
            _isDead = true;
            OnDeath?.Invoke();
        }
    }
}
