using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class ChunkSpawner : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform player;
    [SerializeField] private RoadChunk[] roadChunkPrefabs;
    [SerializeField] private List<ChunkPatternSO> patterns;

    [Header("Spawn")]
    [SerializeField] private int startChunkCount = 5;
    [SerializeField] private float spawnAheadDistance = 80f;

    [Header("Difficulty")]
    [SerializeField] private float distancePerDifficulty = 150f;
    [SerializeField] private int maxDifficulty = 10;

    private Vector3 nextSpawnPosition;
    private ChunkPatternSO lastPattern;

    private void Start()
    {
        for (int i = 0; i < startChunkCount; i++)
        {
            SpawnChunk();
        }
    }

    private void Update()
    {
        if (player.position.z + spawnAheadDistance >= nextSpawnPosition.z)
        {
            SpawnChunk();
        }
    }

    private void SpawnChunk()
    {
        RoadChunk roadPrefab = roadChunkPrefabs[Random.Range(0, roadChunkPrefabs.Length)];
        RoadChunk chunk = Instantiate(roadPrefab, nextSpawnPosition, Quaternion.identity, transform);

        int difficulty = GetDifficulty();
        ChunkPatternSO pattern = GetPatternByDifficulty(difficulty);

        chunk.Build(pattern);

        nextSpawnPosition = chunk.NextSpawnPoint.position + Vector3.forward * pattern.chunkSpacing;
        lastPattern = pattern;
    }

    private int GetDifficulty()
    {
        if (player == null) return 0;

        int difficulty = Mathf.FloorToInt(player.position.z / distancePerDifficulty);
        return Mathf.Clamp(difficulty, 0, maxDifficulty);
    }

    private ChunkPatternSO GetPatternByDifficulty(int difficulty)
    {
        var validPatterns = patterns
            .Where(p =>
                p.minDifficulty <= difficulty &&
                p.maxDifficulty >= difficulty &&
                p != lastPattern)
            .ToList();

        if (validPatterns.Count == 0)
            validPatterns = patterns;

        return GetWeightedRandom(validPatterns);
    }

    private ChunkPatternSO GetWeightedRandom(List<ChunkPatternSO> list)
    {
        float totalWeight = list.Sum(p => p.weight);
        float randomValue = Random.Range(0, totalWeight);

        float current = 0f;

        foreach (var pattern in list)
        {
            current += pattern.weight;

            if (randomValue <= current)
                return pattern;
        }

        return list[0];
    }
}