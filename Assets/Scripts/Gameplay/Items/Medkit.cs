using UnityEngine;

public class Medkit : Collectable
{
    [field: SerializeField] public int HealAmount { get; private set; } = 2;
}