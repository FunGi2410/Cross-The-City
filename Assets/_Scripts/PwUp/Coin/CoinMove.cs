using UnityEngine;

public class CoinMove : MonoBehaviour
{
    [SerializeField] float speed;

    private void Update()
    {
        MoveToPlayer();
    }

    void MoveToPlayer()
    {
        transform.position = Vector3.MoveTowards(transform.position, Bike.Instance.transform.position, this.speed * Time.deltaTime);
    }
}
