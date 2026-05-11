using UnityEngine;

public class Road : MonoBehaviour
{
    RoadSpawner roadSpawner;
    Vector3 nextSpawnPoint;

    private void Awake()
    {
        roadSpawner = FindFirstObjectByType<RoadSpawner>();
        nextSpawnPoint = transform.GetChild(0).transform.position;
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Bike")) return;
        if(other.transform.position.z > nextSpawnPoint.z)
        {
            Debug.Log("Road Spawn");
            roadSpawner.Spawn();
            Destroy(gameObject, 2);
        }
    }
}
