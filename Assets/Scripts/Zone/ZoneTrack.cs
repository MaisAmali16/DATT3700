using UnityEngine;
using System.Collections;

public class ZoneTracker : MonoBehaviour
{
    public MonoBehaviour[] trackingScripts; // Gaze, Turn, Distance trackers
    public float activeDuration = 10f;

    private bool hasTracked = false; // only track once
    private bool isActive = false;


    private void Start()
    {
        // Make sure all trackers are off at the start
        foreach (var tracker in trackingScripts)
            tracker.enabled = false;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isActive && !hasTracked)
        {
            StartCoroutine(ActivateZone());
        }
    }

    private IEnumerator ActivateZone()
    {
        isActive = true;

        // Enable all trackers
        foreach (var tracker in trackingScripts)
            tracker.enabled = true;

        Debug.Log("Zone tracking started: " + gameObject.name);

        yield return new WaitForSeconds(activeDuration);

        // Collect data
        ZoneData data = new ZoneData();
        data.zoneName = gameObject.name;

        foreach (var tracker in trackingScripts)
        {
            if (tracker is GazeTracker gaze)
                data.gazeTime = gaze.stareTime;

            if (tracker is TurnTracking turn)
                data.gazeCount = turn.gazeCount;

            if (tracker is DistanceTracker dist)
                data.averageDistance = dist.averageDistance;
        }

        // Store data globally
        PlayerDataManager.Instance.StoreZoneData(data);
        //debug show what the info is
        PlayerDataManager.Instance.PrintAllZones(); 

        // Disable trackers
        foreach (var tracker in trackingScripts)
            tracker.enabled = false;

        isActive = false;
        hasTracked = true;

        Debug.Log("Zone tracking ended: " + gameObject.name);

         // -------------------------------
    // Check if all 4 zones have been tracked
    if (PlayerDataManager.Instance.zoneDataList.Count >= 4)
    {
        var result = PlayerDataManager.Instance.GetPlayerProfile();
        Debug.Log($"=== All Zones Complete ===");
        Debug.Log($"Player profile: {result.profile}");
        if (result.strongestZone != null)
            Debug.Log($"Strongest reaction was in zone: {result.strongestZone.zoneName}");
    }
    }
}