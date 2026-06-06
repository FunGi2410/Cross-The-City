using UnityEngine;
using System;

public class VehicleActivator : MonoBehaviour
{
    public event Action OnPlayerDetected;

    [SerializeField] private float triggerDistance = 20f;

    private bool isTriggered;

    public void SetTriggerDistance(float value)
    {
        triggerDistance = value;
    }

    private void Update()
    {
        if (isTriggered) return;
        if (Bike.Instance == null) return;

        float playerZ = Bike.Instance.transform.position.z;

        if (playerZ >= transform.position.z - triggerDistance)
        {
            isTriggered = true;
            OnPlayerDetected?.Invoke();
        }
    }
}