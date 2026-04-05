using System;
using UnityEngine;

public class Medkit : MonoBehaviour, ICollectable
{
    [SerializeField] private int _healAmount = 2;

    public event Action<ICollectable> Collected;

    public int HealAmount => _healAmount;

    public void RaiseCollected()
    {
        Collected?.Invoke(this);
    }
}
