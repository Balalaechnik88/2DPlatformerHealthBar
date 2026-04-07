using UnityEngine;

public class CoinSpawner : CollectableSpawner
{
    [SerializeField] private Coin _coinPrefab;

    private void Start()
    {
        Spawn();
    }

    private void Spawn()
    {
        if (_coinPrefab == null || HasSpawnPoints == false)
            return;

        for (int i = 0; i < SpawnPoints.Length; i++)
        {
            Transform spawnPoint = SpawnPoints[i];
            SpawnCollectable(_coinPrefab, spawnPoint);
        }
    }
}