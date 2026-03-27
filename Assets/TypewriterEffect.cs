using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.UI;

public class TypewriterEffect : MonoBehaviour
{
    private TextMeshProUGUI tmProText;
    public float delay = 0.1f;
    public Button startButton;

    void Start()
    {
        tmProText = GetComponent<TextMeshProUGUI>();
        if (tmProText != null)
        {
            tmProText.maxVisibleCharacters = 0;
			if (tmProText.text == "THE CORRIDOR")
            {
                tmProText.maxVisibleCharacters = 0;
                StartCoroutine(TypeCharacters());
            }
        }
        if (startButton != null)
        {
            startButton.onClick.AddListener(OnButtonClick);
        }
    }

    void OnButtonClick()
    {
        if (tmProText != null)
        {
            StartCoroutine(TypeCharacters());
        }
    }

    IEnumerator TypeCharacters()
    {
        tmProText.ForceMeshUpdate();
        int totalVisibleCharacters = tmProText.textInfo.characterCount;

        for (int i = 0; i <= totalVisibleCharacters; i++)
        {
            tmProText.maxVisibleCharacters = i;
            yield return new WaitForSeconds(delay);
        }
    }
}