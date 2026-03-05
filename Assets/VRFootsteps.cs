using UnityEngine;

public class VRFootsteps : MonoBehaviour
{
    public AudioSource playerAudio;
    public AudioClip leftFoot;
    public AudioClip rightFoot;
    public float stepDistance = 1.5f;

    private Vector3 lastPosition;
    private float distanceTraveled = 0f;
    private bool isLeftFoot = true;

    void Start()
    {
        lastPosition = transform.position;
    }

    void Update()
    {
        float distanceThisFrame = Vector3.Distance(transform.position, lastPosition);
        distanceTraveled += distanceThisFrame;
        lastPosition = transform.position;

        if (distanceTraveled >= stepDistance)
        {
            PlayFootstep();
            distanceTraveled = 0f; 
        }
    }

    void PlayFootstep()
    {
        if (isLeftFoot)
        {
            playerAudio.PlayOneShot(leftFoot);
        }
        else
        {
            playerAudio.PlayOneShot(rightFoot);
        }
        
        isLeftFoot = !isLeftFoot;
    }
}