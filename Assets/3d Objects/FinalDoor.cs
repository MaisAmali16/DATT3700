using UnityEngine;

public class FinalDoor : MonoBehaviour
{
    [SerializeField] private BreakerPuzzleManager puzzleManager;
    [SerializeField] private Animator myDoor = null;

    [SerializeField] private bool finalTrigger = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        if(puzzleManager == null)
        {
            Debug.LogError("PuzzleManager NOT Assigned");
            return;
        }

        if (puzzleManager.IsPuzzleSolved)
        {
            
            if (finalTrigger)
            {
                myDoor.Play("FinalDoor", 0, 0.0f);
                gameObject.SetActive(false);
            }
            else
            {
                Debug.Log("Trigger is OFF - door wont open");
            }

           
        }
        else
        {
            Debug.Log("Door locked - puzzle not solved");
        }


    }
}



