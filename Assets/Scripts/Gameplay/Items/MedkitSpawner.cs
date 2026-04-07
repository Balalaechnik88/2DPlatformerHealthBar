using UnityEngine;

public class MedkitSpawner : CollectableSpawner
{
    [SerializeField] private Medkit _medkitPrefab;
    [SerializeField] private int _minCount = 3;
    [SerializeField] private int _maxCount = 7;

    private void Start()
    {
        SpawnRandom();
    }

    private void SpawnRandom()
    {
        if (_medkitPrefab == null || HasSpawnPoints == false)
            return;

        int spawnCount = Random.Range(_minCount, _maxCount + 1);
        spawnCount = Mathf.Min(spawnCount, SpawnPoints.Length);

        int[] shuffledIndices = CreateShuffledIndices(SpawnPoints.Length);

        for (int i = 0; i < spawnCount; i++)
        {
            Transform spawnPoint = SpawnPoints[shuffledIndices[i]];
            SpawnCollectable(_medkitPrefab, spawnPoint);
        }
    }

    private int[] CreateShuffledIndices(int length)
    {
        int[] indices = new int[length];

        for (int i = 0; i < length; i++)
            indices[i] = i;

        for (int i = 0; i < length; i++)
        {
            int swapIndex = Random.Range(i, length);
            (indices[i], indices[swapIndex]) = (indices[swapIndex], indices[i]);
        }

        return indices;
    }
}