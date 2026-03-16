using UnityEngine;

public class Room2Trigger : MonoBehaviour
{
    public Room2Manager room2Manager;
    public AudioSource leftDoorAudio;  // Slot for Left Door
    public AudioSource rightDoorAudio; // Slot for Right Door

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MainCamera") || other.CompareTag("Player"))
        {
            Debug.Log("Player entered Room 2 trigger");
            room2Manager.StartRoom();

            // Play both sounds at once!
            if (leftDoorAudio != null) leftDoorAudio.Play();
            if (rightDoorAudio != null) rightDoorAudio.Play();
        }
    }
}