using UnityEngine;

public abstract class HealthIndicatorBase : MonoBehaviour
{
    [SerializeField] private Health _health;

    protected Health Health => _health;

    protected virtual void Awake()
    {
        if (_health == null)
        {
            Debug.LogError($"[{GetType().Name}] Health не назначен. Скрипт отключён.", this);
            enabled = false;
            return;
        }

        OnAwakeValidated();
    }

    private void OnEnable()
    {
        if (_health == null)
            return;

        _health.HealthChanged += OnHealthChanged;
        Apply(_health.CurrentHealth, _health.MaxHealth);
    }

    private void OnDisable()
    {
        if (_health == null)
            return;

        _health.HealthChanged -= OnHealthChanged;
    }

    private void OnHealthChanged(int current, int max)
    {
        Apply(current, max);
    }

    protected virtual void OnAwakeValidated() { }

    protected abstract void Apply(int currentHealth, int maxHealth);
}
