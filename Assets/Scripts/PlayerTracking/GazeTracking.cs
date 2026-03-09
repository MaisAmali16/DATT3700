using UnityEngine;
public class GazeTracker : MonoBehaviour
{
    public float stareTime = 0f;
    public float triggerTime = 3f;
    void Update()
    {
        Camera cam = Camera.main;
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject == gameObject)
            {
                stareTime += Time.deltaTime;
                Debug.Log("Staring at object: " + stareTime);
                if (stareTime >= triggerTime)
                {
                    Debug.Log("Triggered!");
                }
            }
            else
            {
                stareTime = 0f;
            }
        }
    }
}