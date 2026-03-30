using UnityEngine;

public class CoridorDoor : MonoBehaviour {

    [SerializeField] private Animator myDoor = null;

    [SerializeField] private bool openTrigger = false;
    [SerializeField] private bool closeTrigger = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (openTrigger)
            {
                myDoor.Play("DoorOpenAnimation", 0, 0.0f);
                gameObject.SetActive(false);
            }
            else if (closeTrigger)
            {
                myDoor.Play("DoorCloseAnimation", 0, 0.0f);
                gameObject.SetActive(false);
            }
        }

    }

}

