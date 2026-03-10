/* using UnityEngine;

public class BreakerButton : MonoBehaviour
{
    public bool isCorrectButton = false;

    void OnMouseDown()
    {
        if (isCorrectButton)
            Debug.Log("CORRECT BUTTON PRESSED");
        else
            Debug.Log("Wrong: " + gameObject.name);
    }
} */

using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class BreakerButton : MonoBehaviour
{
    public bool isCorrectButton = false;

    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRSimpleInteractable interactable;

    void Start()
    {
        interactable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRSimpleInteractable>();
        interactable.selectEntered.AddListener(OnButtonPressed);
    }

    void OnButtonPressed(SelectEnterEventArgs args)
    {
        if (isCorrectButton)
            Debug.Log("CORRECT BUTTON PRESSED");
        else
            Debug.Log("Wrong: " + gameObject.name);
    }
}