using UnityEngine;
using UnityEngine.UI;

public abstract class HealthButtonBase : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private Health _health;
    [SerializeField] private int _amount = 10;

    protected Health Health => _health;
    protected int Amount => _amount;

    private void Awake()
    {
        if (_button == null || _health == null)
        {
            Debug.LogError($"[{GetType().Name}] Не назначены Button/Health. Скрипт отключён.", this);
            enabled = false;
            return;
        }
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(OnClick);
    }

    private void OnDisable()
    {
        if (_button != null)
            _button.onClick.RemoveListener(OnClick);
    }

    private void OnClick()
    {
        Apply(Health, Amount);
    }

    protected abstract void Apply(Health health, int amount);
}
