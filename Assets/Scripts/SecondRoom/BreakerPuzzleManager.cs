using UnityEngine;

public class BreakerPuzzleManager : MonoBehaviour
{
    [SerializeField] private Animator mydoor = null;
    public int totalCorrectButtons = 3; // number of panels / correct buttons
    private int currentProgress = 0;

    public RoomLightController lightController;

    public void RegisterCorrectPress()
    {
        currentProgress++;
        Debug.Log("Correct sequence progress: " + currentProgress);

        if (currentProgress >= totalCorrectButtons)
        {
            Debug.Log("PUZZLE COMPLETE!");
            lightController.TurnOnLight();
            mydoor.Play("FinalDoor", 0, 0.0f);
        }
    }

    public void RegisterWrongPress()
    {
        Debug.Log("Wrong button pressed — sequence reset.");
        currentProgress = 0;
    }
}