using System;
using UnityEngine;

public abstract class Collectable : MonoBehaviour, ICollectable
{
    public event Action<Collectable> Collected;

    public void RaiseCollected()
    {
        Collected?.Invoke(this);
    }
}