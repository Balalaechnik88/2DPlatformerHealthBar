using UnityEngine;

public class MedkitSpawner : MonoBehaviour
{
    [SerializeField] private Medkit _medkitPrefab;
    [SerializeField] private Transform[] _spawnPoints;

    [SerializeField] private int _minCount = 3;
    [SerializeField] private int _maxCount = 7;

    private void Start()
    {
        SpawnRandom();
    }

    private void SpawnRandom()
    {
        if (_medkitPrefab == null || _spawnPoints == null || _spawnPoints.Length == 0)
            return;

        int spawnCount = Random.Range(_minCount, _maxCount + 1);
        spawnCount = Mathf.Min(spawnCount, _spawnPoints.Length);

        int[] shuffledIndices = CreateShuffledIndices(_spawnPoints.Length);

        for (int spawnIndex = 0; spawnIndex < spawnCount; spawnIndex++)
        {
            Transform spawnPoint = _spawnPoints[shuffledIndices[spawnIndex]];

            if (spawnPoint == null)
                continue;

            Medkit spawnedMedkit = Instantiate(_medkitPrefab, spawnPoint.position, Quaternion.identity);
            spawnedMedkit.Collected += OnCollectableCollected;
        }
    }

    private void OnCollectableCollected(ICollectable collectable)
    {
        collectable.Collected -= OnCollectableCollected;

        if (collectable is MonoBehaviour collectableBehaviour)
            Destroy(collectableBehaviour.gameObject);
    }

    private int[] CreateShuffledIndices(int length)
    {
        int[] indices = new int[length];

        for (int index = 0; index < length; index++)
            indices[index] = index;

        for (int index = 0; index < length; index++)
        {
            int swapIndex = Random.Range(index, length);
            (indices[index], indices[swapIndex]) = (indices[swapIndex], indices[index]);
        }

        return indices;
    }
}
