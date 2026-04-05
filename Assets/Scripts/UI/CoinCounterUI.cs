using TMPro;
using UnityEngine;

public class CoinCounterUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PlayerWallet _wallet;
    [SerializeField] private TMP_Text _coinsText;

    private void Awake()
    {
        if (_wallet == null)
        {
            Debug.LogError($"[{nameof(CoinCounterUI)}] Не назначен PlayerWallet.", this);
            return;
        }

        if (_coinsText == null)
        {
            Debug.LogError($"[{nameof(CoinCounterUI)}] Не назначен TMP_Text.", this);
            return;
        }
    }

    private void OnEnable()
    {
        _wallet.CoinsChanged += UpdateCoinsText;
        UpdateCoinsText(_wallet.Coins);
    }

    private void OnDisable()
    {
        _wallet.CoinsChanged -= UpdateCoinsText;
    }

    private void UpdateCoinsText(int coins)
    {
        _coinsText.text = coins.ToString();
    }
}
