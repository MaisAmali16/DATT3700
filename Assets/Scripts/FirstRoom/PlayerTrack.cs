using UnityEngine;

public class FacePlayer : MonoBehaviour
{
    public Transform playerCamera; // assign your XR camera here

    void Update()
    {
        Vector3 lookDirection = playerCamera.position - transform.position;
        lookDirection.y = 0; // keep only horizontal rotation
        if (lookDirection.sqrMagnitude > 0.001f)
        {
            transform.rotation = Quaternion.LookRotation(lookDirection);
        }
    }
}
