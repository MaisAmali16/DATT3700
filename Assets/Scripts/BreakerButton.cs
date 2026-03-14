using UnityEngine;

public class BreakerButton : MonoBehaviour
{
    public bool isCorrectButton = false;
    public RoomLightController lightController;
    public Renderer ledRenderer;
    public Material ledGreen;
    public Material ledRed;

    void OnMouseDown()
    {
        if (isCorrectButton)
        {
            Debug.Log("CORRECT BUTTON PRESSED");
            lightController.TurnOnLight();
            ledRenderer.material = ledGreen;
        }
        else
        {
            Debug.Log("Wrong: " + gameObject.name);
            ledRenderer.material = ledRed;
        }
    }
}

/*
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class BreakerButton : MonoBehaviour
{
    public bool isCorrectButton = false;
    public RoomLightController lightController;
    public Renderer ledRenderer;
    public Material ledGreen;
    public Material ledRed;

    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRSimpleInteractable interactable;

    void Start()
    {
        interactable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRSimpleInteractable>();
        interactable.selectEntered.AddListener(OnButtonPressed);
    }

    void OnButtonPressed(SelectEnterEventArgs args)
    {
        if (isCorrectButton)
        {
            Debug.Log("CORRECT BUTTON PRESSED");
            lightController.TurnOnLight();
            ledRenderer.material = ledGreen;
        }
        else
        {
            Debug.Log("Wrong: " + gameObject.name);
            ledRenderer.material = ledRed;
        }
    }
}
*/