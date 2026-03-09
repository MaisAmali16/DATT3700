using UnityEngine;

public class DistanceTracker : MonoBehaviour
{
    public Transform playerCamera;
    public float totalDistance = 0f;
    public int sampleCount = 0;
    public float averageDistance = 0f;

    private bool isTracking = false;

    void Update()
    {
        if (playerCamera == null)
            playerCamera = Camera.main.transform;

        Ray ray = new Ray(playerCamera.position, playerCamera.forward);
        RaycastHit hit;

        // Only track distance when player is looking at object
        if (Physics.Raycast(ray, out hit) && hit.collider.gameObject == gameObject)
        {
            float distance = Vector3.Distance(playerCamera.position, transform.position);

            totalDistance += distance;
            sampleCount++;

            averageDistance = totalDistance / sampleCount;
            isTracking = true;
        }
        else
        {
            isTracking = false;
        }
    }
}