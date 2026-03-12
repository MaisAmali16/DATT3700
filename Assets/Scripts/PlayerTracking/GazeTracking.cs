using UnityEngine;

public class GazeTracker : MonoBehaviour
{
    public float stareTime = 0f;
    public Transform cameraTransform; // drag XR camera here

    void OnEnable()
    {
        stareTime = 0f;

        // fallback if not assigned
        if (cameraTransform == null && Camera.main != null)
            cameraTransform = Camera.main.transform;
    }

    void Update()
    {
        if (cameraTransform == null) return;

        Ray ray = new Ray(cameraTransform.position, cameraTransform.forward);
        RaycastHit[] hits = Physics.RaycastAll(ray, 100f); // hit everything in the path

        bool isLooking = false;

        foreach (var hit in hits)
        {
            if (hit.collider.gameObject == gameObject)
            {
                isLooking = true;
                stareTime += Time.deltaTime;
                Debug.Log("Staring at object: " + stareTime);
                break; // stop after hitting this object
            }
        }

      
    }
}