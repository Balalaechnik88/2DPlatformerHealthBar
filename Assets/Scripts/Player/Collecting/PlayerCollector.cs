using System;
using UnityEngine;

public class PlayerCollector : MonoBehaviour
{
    public event Action<ICollectable> CollectableDetected;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out ICollectable collectable) == false)
            return;

        CollectableDetected?.Invoke(collectable);
    }
}
