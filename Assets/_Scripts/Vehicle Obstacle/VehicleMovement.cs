using UnityEngine;

public class VehicleMovement : MonoBehaviour
{
    [SerializeField] private float speed = 6f;
    [SerializeField] private bool canMoveImmediately;
    [SerializeField] private float checkFrontDistance = 4f;

    private bool canMove;
    private Collider mCollider;
    private VehicleActivator activator;

    private void Awake()
    {
        mCollider = GetComponent<Collider>();
        activator = GetComponent<VehicleActivator>();

        if (activator != null)
            activator.OnPlayerDetected += ActiveMovement;

        canMove = canMoveImmediately;
    }

    public void SetMovingType(bool isMoving)
    {
        canMoveImmediately = isMoving;
        canMove = isMoving == false ? false : canMove;
    }

    private void Update()
    {
        if (canMove)
            Move();

        if (Bike.Instance != null && transform.position.z < Bike.Instance.transform.position.z - 5f)
        {
            if (mCollider != null)
                mCollider.enabled = false;

            Destroy(gameObject, 3f);
        }
    }

    private void ActiveMovement()
    {
        canMove = true;
    }

    private void Move()
    {
        if (HasVehicleInFront()) return;

        transform.Translate(Vector3.forward * speed * Time.deltaTime, Space.World);
    }

    private bool HasVehicleInFront()
    {
        if (Physics.Raycast(transform.position, Vector3.forward, out RaycastHit hit, checkFrontDistance))
        {
            return hit.collider.CompareTag("Vehicle");
        }

        return false;
    }

    private void OnDestroy()
    {
        if (activator != null)
            activator.OnPlayerDetected -= ActiveMovement;
    }
}