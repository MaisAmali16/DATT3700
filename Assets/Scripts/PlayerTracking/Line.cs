using UnityEngine;

public class Line : MonoBehaviour
{
    public Transform playerCamera;
    public LineRenderer rayLine;

    void Update()
    {
        Vector3 start = playerCamera.position;
        Vector3 end = playerCamera.position + playerCamera.forward * 5f;

        rayLine.SetPosition(0, start);
        rayLine.SetPosition(1, end);
    }
}
