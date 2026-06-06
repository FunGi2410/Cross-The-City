using UnityEngine;

public class RoadChunk : MonoBehaviour
{
    [Header("Lane Points")]
    [SerializeField] private Transform leftLane;
    [SerializeField] private Transform middleLane;
    [SerializeField] private Transform rightLane;

    [Header("Roots")]
    [SerializeField] private Transform obstacleRoot;
    [SerializeField] private Transform coinRoot;
    [SerializeField] private Transform powerUpRoot;

    [Header("Next Point")]
    [SerializeField] private Transform nextSpawnPoint;

    public Transform NextSpawnPoint => nextSpawnPoint;

    public void Build(ChunkPatternSO pattern)
    {
        SpawnObstacles(pattern);
        SpawnCoins(pattern);
        SpawnPowerUps(pattern);
    }

    private Transform GetLane(LaneType lane)
    {
        return lane switch
        {
            LaneType.Left => leftLane,
            LaneType.Middle => middleLane,
            LaneType.Right => rightLane,
            _ => middleLane
        };
    }

    private void SpawnObstacles(ChunkPatternSO pattern)
    {
        foreach (var data in pattern.obstacles)
        {
            Transform lane = GetLane(data.lane);
            Vector3 pos = lane.position + Vector3.forward * data.zOffset;

            GameObject obj = Instantiate(data.prefab, pos, lane.rotation, obstacleRoot);

            if (obj.TryGetComponent(out VehicleMovement movement))
            {
                movement.SetMovingType(data.isMoving);
            }

            if (obj.TryGetComponent(out VehicleActivator activator))
            {
                activator.SetTriggerDistance(data.triggerDistance);
            }
        }
    }

    private void SpawnCoins(ChunkPatternSO pattern)
    {
        foreach (var data in pattern.coins)
        {
            Transform lane = GetLane(data.lane);

            for (int i = 0; i < data.amount; i++)
            {
                Vector3 pos = lane.position + Vector3.forward * (data.startZ + i * data.spacing);
                Instantiate(data.coinPrefab, pos, Quaternion.identity, coinRoot);
            }
        }
    }

    private void SpawnPowerUps(ChunkPatternSO pattern)
    {
        foreach (var data in pattern.powerUps)
        {
            if (Random.value > data.spawnChance) continue;

            Transform lane = GetLane(data.lane);
            Vector3 pos = lane.position + Vector3.forward * data.zOffset;

            Instantiate(data.prefab, pos, Quaternion.identity, powerUpRoot);
        }
    }
}