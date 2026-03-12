using UnityEngine;

public class TurnTracking : MonoBehaviour
{
    [Header("XR Settings")]
    public Transform cameraTransform; // drag XR camera here

    [Header("Tracking Stats")]
    private bool isLooking = false;   // tracks if player is currently looking
    public int gazeCount = 0;         // how many times the player looked

    void OnEnable()
    {
        // Reset looking state when zone enables this tracker
        isLooking = false;
    }

    void Update()
    {
        if (cameraTransform == null)
        {
            if (Camera.main != null)
                cameraTransform = Camera.main.transform;
            else
                return; // no camera assigned
        }

        Ray ray = new Ray(cameraTransform.position, cameraTransform.forward);
        RaycastHit[] hits = Physics.RaycastAll(ray, 100f); // hit everything along the ray

        bool lookingAtObject = false;

        foreach (var hit in hits)
        {
            if (hit.collider.gameObject == gameObject)
            {
                lookingAtObject = true;

                // Player started looking at the object
                if (!isLooking)
                {
                    isLooking = true;
                    gazeCount++;
                    Debug.Log("Gaze #" + gazeCount);
                }

                break; // stop after hitting this object
            }
        }

        // Player looked away
        if (!lookingAtObject)
            isLooking = false;
    }
}