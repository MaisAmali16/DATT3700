using UnityEngine;

public class RandomBlinkOffset : MonoBehaviour
{
    void Start()
    {
        Animator anim = GetComponent<Animator>();
        if(anim != null)
        {
            anim.Play(0, 0, Random.Range(0f, 1f)); // random start time
        }
    }
}