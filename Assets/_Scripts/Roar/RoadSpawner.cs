using UnityEngine;

public class RoadSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] roadPrefabs;
    Vector3 nextSpawnPoint;
    //VehicleSpawner vehicleSpawner;

    private void Awake()
    {
        //this.vehicleSpawner = GameObject.FindGameObjectWithTag("VehicleSpawner").GetComponent<VehicleSpawner>();
    }
    private void Start()
    {
        this.Spawn();
        this.Spawn();
    }

    public void Spawn()
    {
        int index = Random.Range(0, this.roadPrefabs.Length);
        GameObject tile = Instantiate(this.roadPrefabs[index], this.nextSpawnPoint, Quaternion.identity);
        // Vehicle Spawn
        //this.vehicleSpawner.Spawn(this.nextSpawnPoint);
        this.nextSpawnPoint = tile.transform.GetChild(0).transform.position;

        
    }
}
