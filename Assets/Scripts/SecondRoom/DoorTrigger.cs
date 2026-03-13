using UnityEngine;

public class Room2Trigger : MonoBehaviour
{
    public Room2Manager room2Manager;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("MainCamera") || other.CompareTag("Player"))
        {
            Debug.Log("Player entered Room 2 trigger");
            room2Manager.StartRoom();
        }
    }
}