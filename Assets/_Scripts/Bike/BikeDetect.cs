using UnityEngine;

public class BikeDetect : MonoBehaviour
{
    private Rigidbody bikeBodyRb;
    private Vector3 dirForce = new Vector3(0f, 1f, -0.5f);
    private bool isBreak;
    [SerializeField] private float forceToBreak = 17f;

    private void Awake()
    {
        bikeBodyRb = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        IPowerUp isPowerUp = other.GetComponent<IPowerUp>();
        if (isPowerUp != null)
        {
            isPowerUp.Collect(transform);
            Destroy(other.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isBreak) return;
        if (!collision.gameObject.CompareTag("Road") && bikeBodyRb != null)
        {
            Rigidbody otherRb = collision.rigidbody;
            if (otherRb == null) return;

            // calculate relative velocity between 2 objects
            Vector3 relativeVelocity = bikeBodyRb.linearVelocity - collision.relativeVelocity;

            // calculate mass
            float combinedMass = bikeBodyRb.mass * otherRb.mass / (bikeBodyRb.mass + otherRb.mass);

            Vector3 impactForce = relativeVelocity * combinedMass;

            // magnitude of force more than forceToBreak when bike break
            // if(impactForce.magnitude > forceToBreak)
            // {
            //     isBreak = true;
            //     bikeBodyRb.freezeRotation = false;
            //     bikeBodyRb.AddForce(impactForce.magnitude * dirForce, ForceMode.Impulse);
            //     BikeMovement.canMove = false;
            //     GameManager.instance.GameOver();
            // }
        }
    }
}
