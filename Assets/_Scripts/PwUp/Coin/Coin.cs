using UnityEngine;

public class Coin : MonoBehaviour
{
    CoinMove coinMove;

    private void Awake()
    {
        coinMove = GetComponent<CoinMove>();
        coinMove.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("MagnetDetect"))
        {
            //Debug.Log("Detect coin");
            coinMove.enabled = true;
        }
    }
}
