using UnityEngine;
using System.Collections;

public class RoomLightController : MonoBehaviour
{
    public Light roomLight;
    public float targetIntensity = 3f;
    public float fadeDuration = 2f;

    public void TurnOnLight()
    {
        Debug.Log("TurnOnLight called!");
        if (roomLight == null)
        {
            Debug.LogError("roomLight is NULL!");
            return;
        }
        Debug.Log("Starting coroutine, current intensity: " + roomLight.intensity);
        StartCoroutine(FadeInLight());
    }

    IEnumerator FadeInLight()
    {
        Debug.Log("FadeInLight coroutine started");
        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            roomLight.intensity = Mathf.Lerp(0, targetIntensity, elapsed / fadeDuration);
            yield return null;
        }
        roomLight.intensity = targetIntensity;
        Debug.Log("Fade complete! Final intensity: " + roomLight.intensity);
    }
}