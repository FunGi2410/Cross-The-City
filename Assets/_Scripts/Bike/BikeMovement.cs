using UnityEngine;

public class BikeMovement : MonoBehaviour
{
    [SerializeField] private float maxSpeed;
    [SerializeField] private float moveInput;
    [SerializeField] private float steerInput;
    [SerializeField] private float acceleration;
    [SerializeField] private float steerSpeed;
    [SerializeField] private float horizontalSpeed;
    [SerializeField] private float gravity;

    private float rayLength;
    private float tiltIncrement = 0.09f;
    private float zTiltAngle = 45f;
    private float curVelocityOffset;

    private Vector3 velocity;

    public static bool canMove = true;

    [SerializeField] private Rigidbody sphereRb, bikeBodyRb;

    [SerializeField] LayerMask surfaceMask;
    RaycastHit hit;

    private void Awake()
    {
        rayLength = sphereRb.GetComponent<SphereCollider>().radius + 0.2f;

        sphereRb.transform.parent = null;
        bikeBodyRb.transform.parent = null;
    }

    private void Update()
    {
        steerInput = Input.GetAxisRaw("Horizontal");
        moveInput = Input.GetAxisRaw("Vertical");

        transform.position = sphereRb.transform.position;

        velocity = bikeBodyRb.transform.InverseTransformDirection(bikeBodyRb.linearVelocity);
        curVelocityOffset = velocity.z / maxSpeed;
    }

    private void FixedUpdate()
    {
        Movement();
    }

    void Movement()
    {
        //if (!canMove) return;
        if (Grounded())
        {
            Acceleration();
            Rotation();
        }
        else Gravity();
        Tilt();
    }

    void Acceleration()
    {
        //sphereRb.linearVelocity = Vector3.Lerp(sphereRb.linearVelocity, transform.forward * this.maxSpeed * this.velocity, Time.fixedDeltaTime * this.acceleration);
        sphereRb.linearVelocity = Vector3.Lerp(sphereRb.linearVelocity, moveInput * this.maxSpeed * transform.forward, Time.fixedDeltaTime * this.acceleration);

        //mRb.velocity = Vector3.Lerp(mRb.velocity, transform.forward * this.MaxSpeed * this.velocity, Time.fixedDeltaTime * this.acceleration);
        //sphereRb.transform.Translate(sphereRb.transform.forward * this.maxSpeed * this.velocity * Time.fixedDeltaTime);
    }

    void Rotation()
    {
        transform.Rotate(0, steerInput * curVelocityOffset * steerSpeed * Time.fixedDeltaTime, 0, Space.World);

        //sphereRb.linearVelocity = Vector3.Lerp(sphereRb.linearVelocity, Vector3.right * this.steerSpeed * this.horizontalSpeed, Time.fixedDeltaTime * this.acceleration);
        //mRb.velocity = Vector3.Lerp(mRb.velocity, Vector3.right * this.SteerSpeed * this.horizontalSpeed, Time.fixedDeltaTime * this.acceleration);

        // Rotate Handle
    }

    bool Grounded()
    {
        return Physics.Raycast(sphereRb.position, Vector3.down, out hit, rayLength, surfaceMask);
    }

    void Gravity()
    {
        sphereRb.AddForce(gravity * Vector3.zero, ForceMode.Acceleration);
    }

    void Tilt()
    {
        float xRot = (Quaternion.FromToRotation(bikeBodyRb.transform.up, hit.normal) * bikeBodyRb.transform.rotation).eulerAngles.x;
        float zRot = 0f;

        if (curVelocityOffset > 0)
            zRot = -zTiltAngle * steerInput * this.curVelocityOffset;

        Quaternion targetRot = Quaternion.Slerp(bikeBodyRb.transform.rotation, Quaternion.Euler(xRot, transform.eulerAngles.y, zRot), tiltIncrement);
        Quaternion newRot = Quaternion.Euler(targetRot.eulerAngles.x, transform.eulerAngles.y, targetRot.eulerAngles.z); ;

        bikeBodyRb.MoveRotation(newRot);

        /*if (this.curVelocityOffset > 0)
            zRot = -this.zTiltAngle * this.steerSpeed * this.curVelocityOffset;
        if (!isBreak)
        {
            zRot = -this.zTiltAngle * this.steerSpeed;

            Quaternion targetRot = Quaternion.Slerp(this.bikeBodyRb.transform.rotation, Quaternion.Euler(xRot, transform.eulerAngles.y, zRot), this.tiltIncrement);
            newRot = Quaternion.Euler(targetRot.eulerAngles.x, transform.eulerAngles.y, targetRot.eulerAngles.z);
        }
        else
        {
            //this.sphereRb.AddForce(transform.up * 2, ForceMode.Impulse);
            zRot = this.bikeBodyRb.transform.rotation.z;
            //zRot = zTiltAngle;
            xRot = this.sphereRb.transform.rotation.x;
            Quaternion targetRot = Quaternion.Slerp(this.bikeBodyRb.transform.rotation, Quaternion.Euler(xRot, transform.eulerAngles.y, zRot), this.tiltIncrement);
            newRot = Quaternion.Euler(targetRot.eulerAngles.x, transform.eulerAngles.y, targetRot.eulerAngles.z);
        }*/
    }
}
