using UnityEngine;
using System.Collections;

public class ZoneTracker : MonoBehaviour
{
    public MonoBehaviour[] trackingScripts; // Your gaze/distance trackers
    public float activeDuration = 10f; // seconds
    private bool isActive = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isActive)
        {
            StartCoroutine(ActivateZone());
        }
    }

    private IEnumerator ActivateZone()
{
    isActive = true;

    foreach (var tracker in trackingScripts)
        tracker.enabled = true;

    Debug.Log("Zone tracking started: " + gameObject.name);

    yield return new WaitForSeconds(activeDuration);

    foreach (var tracker in trackingScripts)
        tracker.enabled = false;

    // Collect data
    ZoneData data = new ZoneData();
    data.zoneName = gameObject.name;

    // Example: get values from your tracker scripts
    foreach (var tracker in trackingScripts)
    {
        if (tracker is TurnTracking gaze)
            data.gazeCount = gaze.gazeCount;
        if (tracker is GazeTracker stare)
            data.stareTime = stare.stareTime;
        if (tracker is DistanceTracker dist)
            data.averageDistance = dist.averageDistance;
    }

    PlayerDataManager.Instance.StoreZoneData(data);

    isActive = false;
}
}