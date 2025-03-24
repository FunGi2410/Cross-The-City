using UnityEngine;

public class Road : MonoBehaviour
{
    RoadSpawner roadSpawner;
    Vector3 nextSpawnPoint;

    private void Awake()
    {
        roadSpawner = GameObject.FindFirstObjectByType<RoadSpawner>();
        nextSpawnPoint = transform.GetChild(0).transform.position;
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Bike")) return;
        if(other.transform.position.z > nextSpawnPoint.z)
        {
            roadSpawner.Spawn();
            Destroy(gameObject, 2);
        }
    }
}
