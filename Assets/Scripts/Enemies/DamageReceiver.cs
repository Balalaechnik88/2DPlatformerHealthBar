using UnityEngine;

public class DamageReceiver : MonoBehaviour
{
    [SerializeField] private Health _health;

    public Health Health => _health;

    private void Awake()
    {
        if (_health == null)
        {
            Debug.LogError($"[{nameof(DamageReceiver)}] Не назначен Health. Скрипт отключён.", this);
            enabled = false;
        }
    }
}