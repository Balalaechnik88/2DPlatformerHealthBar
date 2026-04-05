using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField] private Coin _coinPrefab;
    [SerializeField] private Transform[] _spawnPoints;

    private void Start()
    {
        Spawn();
    }

    private void Spawn()
    {
        if (_coinPrefab == null || _spawnPoints == null)
            return;

        for (int spawnPointIndex = 0; spawnPointIndex < _spawnPoints.Length; spawnPointIndex++)
        {
            Transform spawnPoint = _spawnPoints[spawnPointIndex];
            if (spawnPoint == null)
                continue;

            Coin spawnedCoin = Instantiate(_coinPrefab, spawnPoint.position, Quaternion.identity);
            spawnedCoin.Collected += OnCollectableCollected;
        }
    }

    private void OnCollectableCollected(ICollectable collectable)
    {
        collectable.Collected -= OnCollectableCollected;

        if (collectable is MonoBehaviour collectableBehaviour)
            Destroy(collectableBehaviour.gameObject);
    }
}
