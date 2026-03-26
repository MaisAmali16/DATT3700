using UnityEngine;

public class BreakerPuzzleManager : MonoBehaviour
{
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
        }
    }

    public void RegisterWrongPress()
    {
        Debug.Log("Wrong button pressed — sequence reset.");
        currentProgress = 0;
    }
}