using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "ChunkPatternSO", menuName = "Scriptable Objects/ChunkPatternSO")]
public class ChunkPatternSO : ScriptableObject
{
    public string patternName;

    [Header("Difficulty")]
    public int minDifficulty = 0;
    public int maxDifficulty = 10;
    public float weight = 1f;

    [Header("Objects")]
    public List<ObstacleSpawnData> obstacles = new();
    public List<CoinSpawnData> coins = new();
    public List<PowerUpSpawnData> powerUps = new();

    [Header("Rules")]
    public bool allowMovingVehicle = true;
    public float chunkSpacing = 0f;
}