using UnityEngine;
using UnityEngine.UI;

public class HealthBarIndicator : HealthIndicatorBase
{
    [SerializeField] protected Slider _slider;

    protected override void OnAwakeValidated()
    {
        if (_slider == null)
        {
            Debug.LogError($"[{nameof(HealthBarIndicator)}] Slider не назначен. Скрипт отключён.", this);
            enabled = false;
        }
    }

    protected override void Apply(int currentHealth, int maxHealth)
    {
        if (_slider == null)
            return;

        _slider.value = GetNormalized(currentHealth, maxHealth);
    }

    protected static float GetNormalized(int currentHealth, int maxHealth)
    {
        if (maxHealth <= 0)
            return 0f;

        return Mathf.Clamp01((float)currentHealth / maxHealth);
    }
}
