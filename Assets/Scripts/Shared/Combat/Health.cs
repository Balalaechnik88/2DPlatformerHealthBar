using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private int _maxHealth = 100;

    [Header("Runtime (read only)")]
    [SerializeField] private int _currentHealth;

    public event Action<int, int> HealthChanged;
    public event Action Died;

    public int CurrentHealth => _currentHealth;
    public int MaxHealth => _maxHealth;

    private void Awake()
    {
        if (_maxHealth <= 0)
        {
            Debug.LogError($"[{nameof(Health)}] MaxHealth должен быть > 0. Скрипт отключён.", this);
            enabled = false;
            return;
        }

        _currentHealth = _maxHealth;
    }

    private void OnEnable()
    {
        if (enabled == false)
            return;

        HealthChanged?.Invoke(_currentHealth, _maxHealth);
    }

    public void TakeDamage(int damage)
    {
        if (damage <= 0 || _currentHealth <= 0)
            return;

        int newHealth = Mathf.Max(_currentHealth - damage, 0);

        if (newHealth == _currentHealth)
            return;

        _currentHealth = newHealth;
        HealthChanged?.Invoke(_currentHealth, _maxHealth);

        if (_currentHealth == 0)
            Died?.Invoke();
    }

    public void Heal(int amount)
    {
        if (amount <= 0 || _currentHealth <= 0)
            return;

        int newHealth = Mathf.Min(_currentHealth + amount, _maxHealth);

        if (newHealth == _currentHealth)
            return;

        _currentHealth = newHealth;
        HealthChanged?.Invoke(_currentHealth, _maxHealth);
    }
}
