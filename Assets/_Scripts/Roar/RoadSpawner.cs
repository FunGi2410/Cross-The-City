using UnityEngine;

public class RoadSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] roadPrefabs;
    Vector3 nextSpawnPoint;
    VehicleSpawner vehicleSpawner;

    private void Awake()
    {
        vehicleSpawner = GameObject.FindGameObjectWithTag("VehicleSpawner").GetComponent<VehicleSpawner>();
    }
    private void Start()
    {
        Spawn();
        Spawn();
    }

    public void Spawn()
    {
        int index = Random.Range(0, roadPrefabs.Length);
        GameObject tile = Instantiate(roadPrefabs[index], nextSpawnPoint, Quaternion.identity);
        // Vehicle Spawn
        vehicleSpawner.Spawn(nextSpawnPoint);
        nextSpawnPoint = tile.transform.GetChild(0).transform.position;

        
    }
}
