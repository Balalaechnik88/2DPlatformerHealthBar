using TMPro;
using UnityEngine;

public class HealthTextIndicator : HealthIndicatorBase
{
    [Header("UI")]
    [SerializeField] private TMP_Text _text;

    protected override void OnAwakeValidated()
    {
        if (_text == null)
        {
            Debug.LogError($"[{nameof(HealthTextIndicator)}] Не назначен TMP_Text. Скрипт отключён.", this);
            enabled = false;
        }
    }

    protected override void Apply(int currentHealth, int maxHealth)
    {
        if (_text == null)
            return;

        _text.text = $"{currentHealth}/{maxHealth}";
    }
}
