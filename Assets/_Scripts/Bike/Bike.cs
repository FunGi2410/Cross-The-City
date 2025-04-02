using UnityEngine;
using TMPro;

public class Bike : MonoBehaviour
{
    public static Bike Instance;

    [SerializeField] private int coinNumber;
    [SerializeField] private TextMeshProUGUI coinText;

    private int distance;
    private float startPosZ;
    [SerializeField] private TextMeshProUGUI distanceText;

    public int Distance { get => distance; }
    public int CoinNumber { get => coinNumber; }

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        startPosZ = transform.position.z;
    }

    private void Update()
    {
        // Calculate & Show distance
        distance = Mathf.RoundToInt(transform.position.z - startPosZ);
        if (distanceText)
            distanceText.text = Distance + " m";

        // Show coin
        if (coinText)
            coinText.text = CoinNumber.ToString();
    }

    public void CollectCoin(int amount)
    {
        coinNumber += amount;
    }
}
