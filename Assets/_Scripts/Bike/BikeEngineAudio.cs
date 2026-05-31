using UnityEngine;

public class BikeEngineAudio : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField] private Rigidbody sphereRb;
    [SerializeField] private AudioSource engineSource;
    [SerializeField] private AudioSource revSource;

    [Header("Engine Settings")]
    [SerializeField] private float maxSpeed = 30f;
    [SerializeField] private float minPitch = 0.8f;
    [SerializeField] private float maxPitch = 2.0f;
    [SerializeField] private float minVolume = 0.25f;
    [SerializeField] private float maxVolume = 1f;
    [SerializeField] private float smoothSpeed = 5f;

    [Header("Rev Settings")]
    [SerializeField] private float revThreshold = 0.75f;
    [SerializeField] private float revCooldown = 0.4f;

    private float lastRevTime;

    // private void Start()
    // {
    //     if (engineSource != null && !engineSource.isPlaying)
    //         engineSource.Play();
    // }

    private void Update()
    {
        float speed = sphereRb.linearVelocity.magnitude;
        float speed01 = Mathf.Clamp01(speed / maxSpeed);
        float targetPitch = Mathf.Lerp(minPitch, maxPitch, speed01);
        float targetVolume = Mathf.Lerp(minVolume, maxVolume, speed01);

        engineSource.pitch = Mathf.Lerp(
            engineSource.pitch,
            targetPitch,
            Time.deltaTime * smoothSpeed
        );

        engineSource.volume = Mathf.Lerp(
            engineSource.volume,
            targetVolume,
            Time.deltaTime * smoothSpeed
        );
        HandleRevSound(speed01);
    }

    private void HandleRevSound(float speed01)
    {
        bool pressGas = Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow);

        if (pressGas && speed01 < revThreshold && Time.time > lastRevTime + revCooldown)
        {
            revSource.pitch = Random.Range(0.9f, 1.15f);
            revSource.PlayOneShot(revSource.clip);

            lastRevTime = Time.time;
        }
    }
}