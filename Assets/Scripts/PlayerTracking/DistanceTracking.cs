using UnityEngine;

public class DistanceTracker : MonoBehaviour
{
    public Transform cameraTransform; // drag XR Origin Main Camera here

    [Header("Distance Stats")]
    public float totalDistance = 0f;
    public int sampleCount = 0;
    public float averageDistance = 0f;

    void OnEnable()
    {
        // reset stats when the zone activates this tracker
        totalDistance = 0f;
        sampleCount = 0;
        averageDistance = 0f;

        if (cameraTransform == null && Camera.main != null)
            cameraTransform = Camera.main.transform;
    }

    void Update()
    {
        if (cameraTransform == null) return;

        float distance = Vector3.Distance(cameraTransform.position, transform.position);

        totalDistance += distance;
        sampleCount++;
        averageDistance = totalDistance / sampleCount;
    }
}