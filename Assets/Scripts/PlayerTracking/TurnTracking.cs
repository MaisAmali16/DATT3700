using UnityEngine;

public class TurnTracking : MonoBehaviour
{
    private bool isLooking = false; // Tracks if player is currently looking
    public int gazeCount = 0;       // How many times the player looked

    void Update()
    {
        Camera cam = Camera.main;
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit) && hit.collider.gameObject == gameObject)
        {
            // Player started looking at object
            if (!isLooking)
            {
                isLooking = true;
                gazeCount++;
                Debug.Log("Gaze #" + gazeCount);
            }
        }
        else
        {
            // Player looked away
            isLooking = false;
        }
    }
}
