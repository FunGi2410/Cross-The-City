using UnityEngine;

[System.Serializable]
public class ObstacleSpawnData
{
    public GameObject prefab;
    public LaneType lane;
    public float zOffset;
    public bool isMoving;
    public float triggerDistance = 20f;
}

[System.Serializable]
public class CoinSpawnData
{
    public GameObject coinPrefab;
    public LaneType lane;
    public float startZ;
    public int amount = 5;
    public float spacing = 1.5f;
}

[System.Serializable]
public class PowerUpSpawnData
{
    public GameObject prefab;
    public LaneType lane;
    public float zOffset;
    public float spawnChance = 0.1f;
}