using UnityEngine;

public class DamageReceiver : MonoBehaviour
{
    [SerializeField] private Health _health;

    public Health Health => _health;

    private void Awake()
    {
        if (_health == null)
            _health = GetComponent<Health>();

        if (_health == null)
            _health = GetComponentInParent<Health>();

        if (_health == null)
        {
            Debug.LogError($"[{nameof(DamageReceiver)}] Не найден Health. Скрипт отключён.", this);
            enabled = false;
        }
    }
}
