using UnityEngine;

public class PlayerHub : MonoBehaviour
{
    [SerializeField] private PlayerCollector _collector;
    [SerializeField] private PlayerWallet _wallet;
    [SerializeField] private Health _health;

    private void Awake()
    {
        if (_collector == null)
            Debug.LogError($"[{nameof(PlayerHub)}] PlayerCollector не назначен.", this);
        if (_wallet == null)
            Debug.LogError($"[{nameof(PlayerHub)}] PlayerWallet не назначен.", this);
        if (_health == null)
            Debug.LogError($"[{nameof(PlayerHub)}] Health не назначен.", this);
    }

    private void OnEnable()
    {
        if (_collector != null)
            _collector.CollectableDetected += OnCollectableDetected;
    }

    private void OnDisable()
    {
        if (_collector != null)
            _collector.CollectableDetected -= OnCollectableDetected;
    }

    private void OnCollectableDetected(ICollectable collectable)
    {
        if (collectable == null)
            return;

        if (collectable is Coin coin)
        {
            _wallet.AddCoins(coin.Value);
            coin.RaiseCollected();
            return;
        }

        if (collectable is Medkit medkit)
        {
            if (CanUseMedkit() == false)
                return;

            _health.Heal(medkit.HealAmount);
            medkit.RaiseCollected();
        }
    }

    private bool CanUseMedkit()
    {
        if (_health == null)
            return false;

        if (_health.CurrentHealth <= 0)
            return false;

        if (_health.CurrentHealth >= _health.MaxHealth)
            return false;

        return true;
    }
}
