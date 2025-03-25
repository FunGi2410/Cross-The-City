using System.Collections;
using UnityEngine;

public class RacingBoySpawner : MonoBehaviour
{
    [SerializeField] GameObject racingBoyPref;
    [SerializeField] private float timeToSpawn = 4f;
    float firtRacingXAxisPos;

    private Vector3 randomPosSpawn;

    private Vector3 sizeRoad;
    private BoxCollider boxCollider;

    [Header("Get size road")]
    public GameObject roadObj;

    private void Awake()
    {
        boxCollider = roadObj.GetComponent<BoxCollider>();
        sizeRoad = boxCollider.size;
    }

    private void Start()
    {
        StartCoroutine(SpawnWithTime());
    }

    void Spawn()
    {
        randomPosSpawn = new Vector3(Random.Range(-sizeRoad.x / 2, sizeRoad.x / 2), 0.05f, transform.position.z);
        if(Mathf.Abs(randomPosSpawn.x - firtRacingXAxisPos) < 2)
        {
            Spawn();
        }
        else
        {
            GameObject racingBoy = Instantiate(racingBoyPref, randomPosSpawn, Quaternion.identity);
            firtRacingXAxisPos = racingBoy.transform.position.x;
        }
    }

    IEnumerator SpawnWithTime()
    {
        Spawn();
        yield return new WaitForSeconds(timeToSpawn);
        StartCoroutine(SpawnWithTime());
    }
}
