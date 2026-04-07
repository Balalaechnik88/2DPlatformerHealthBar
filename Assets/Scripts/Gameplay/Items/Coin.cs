using UnityEngine;

public class Coin : Collectable
{
    [field: SerializeField] public int Value { get; private set; } = 1;
}