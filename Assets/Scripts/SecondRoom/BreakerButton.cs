// OLD LOGIC 

/* using UnityEngine;

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
} */

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

// NEW LOGIC - MOUSE CLICK 
using UnityEngine;
using System.Collections;

public class BreakerButton : MonoBehaviour
{
    public bool isCorrectButton = false;

    public BreakerPuzzleManager puzzleManager;

    public Renderer ledRenderer;
    public Material ledGreen;
    public Material ledRed;

    private Material ledDefault;

    void Start()
    {
        // Store original material (LED off state)
        ledDefault = ledRenderer.material;
    }

    void OnMouseDown()
    {
        if (isCorrectButton)
        {
            Debug.Log("CORRECT BUTTON PRESSED");
            StartCoroutine(FlashLED(ledGreen));

            puzzleManager.RegisterCorrectPress();
        }
        else
        {
            Debug.Log("WRONG BUTTON PRESSED: " + gameObject.name);
            StartCoroutine(FlashLED(ledRed));

            puzzleManager.RegisterWrongPress();
        }
    }

    IEnumerator FlashLED(Material flashMaterial)
    {
        ledRenderer.material = flashMaterial;

        yield return new WaitForSeconds(1f); // stays for 1 second

        ledRenderer.material = ledDefault; // turn OFF again
    }
}

// NEW LOGIC - VR 

/* using UnityEngine;
using System.Collections;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class BreakerButton : MonoBehaviour
{
    public bool isCorrectButton = false;

    public BreakerPuzzleManager puzzleManager;

    public Renderer ledRenderer;
    public Material ledGreen;
    public Material ledRed;

    private Material ledDefault;

    private XRSimpleInteractable interactable;

    void Start()
    {
        // Store default LED material (OFF state)
        ledDefault = ledRenderer.material;

        // Get XR interactable component
        interactable = GetComponent<XRSimpleInteractable>();

        if (interactable != null)
        {
            interactable.selectEntered.AddListener(OnButtonPressed);
        }
        else
        {
            Debug.LogError("XRSimpleInteractable missing on " + gameObject.name);
        }
    }

    void OnButtonPressed(SelectEnterEventArgs args)
    {
        if (isCorrectButton)
        {
            Debug.Log("CORRECT BUTTON PRESSED (VR)");
            StartCoroutine(FlashLED(ledGreen));

            puzzleManager.RegisterCorrectPress();
        }
        else
        {
            Debug.Log("WRONG BUTTON PRESSED (VR): " + gameObject.name);
            StartCoroutine(FlashLED(ledRed));

            puzzleManager.RegisterWrongPress();
        }
    }

    IEnumerator FlashLED(Material flashMaterial)
    {
        ledRenderer.material = flashMaterial;

        yield return new WaitForSeconds(1f);

        ledRenderer.material = ledDefault;
    }
} */