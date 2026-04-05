using System;
using UnityEngine;

public class Coin : MonoBehaviour, ICollectable
{
    [SerializeField] private int _value = 1;

    public event Action<ICollectable> Collected;

    public int Value => _value;

    public void RaiseCollected()
    {
        Collected?.Invoke(this);
    }
}
