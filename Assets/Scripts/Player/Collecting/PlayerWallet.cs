using System;
using UnityEngine;

public class PlayerWallet : MonoBehaviour
{
    public event Action<int> CoinsChanged;

    public int Coins { get; private set; }

    public void AddCoins(int amount)
    {
        if (amount <= 0)
            return;

        Coins += amount;
        CoinsChanged?.Invoke(Coins);
    }
}
