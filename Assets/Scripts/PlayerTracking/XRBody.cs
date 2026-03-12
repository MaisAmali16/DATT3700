using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class XRBodyFollower : MonoBehaviour
{
    public Transform cameraTransform;

    private CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        float height = Mathf.Clamp(cameraTransform.localPosition.y, 1f, 2f);
        controller.height = height;

        Vector3 center = Vector3.zero;
        center.y = height / 2;
        center.x = cameraTransform.localPosition.x;
        center.z = cameraTransform.localPosition.z;

        controller.center = center;
    }
}
