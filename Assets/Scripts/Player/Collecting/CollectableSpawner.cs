using UnityEngine;

public abstract class CollectableSpawner : MonoBehaviour
{
    [SerializeField] private Transform[] _spawnPoints;

    protected Transform[] SpawnPoints => _spawnPoints;

    protected bool HasSpawnPoints => _spawnPoints != null && _spawnPoints.Length > 0;

    protected TCollectable SpawnCollectable<TCollectable>(TCollectable prefab, Transform spawnPoint)
        where TCollectable : Collectable
    {
        if (prefab == null || spawnPoint == null)
            return null;

        TCollectable spawnedCollectable = Instantiate(prefab, spawnPoint.position, Quaternion.identity);
        spawnedCollectable.Collected += OnCollected;

        return spawnedCollectable;
    }

    private void OnCollected(Collectable collectable)
    {
        collectable.Collected -= OnCollected;
        Destroy(collectable.gameObject);
    }
}