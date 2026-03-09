using UnityEngine;

public class PlaySoundOnEnter : MonoBehaviour
{
    public AudioSource source;

    void Reset()
    {
        source = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        Debug.Log("Calling Play()");
        Debug.Log("source is NULL? " + (source == null));

        if (source == null) return;

        Debug.Log("source.clip is NULL? " + (source.clip == null));
        if (source.clip != null) Debug.Log("source.clip name = " + source.clip.name);

        source.Play();

        Debug.Log("After Play(): source.isPlaying = " + source.isPlaying);
    }

    void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        if (source != null) source.Stop();
    }
}