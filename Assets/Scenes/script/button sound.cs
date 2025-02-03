using UnityEngine;
using UnityEngine.UI;

public class MainMenuButtonSound : MonoBehaviour
{
    [SerializeField] private AudioClip buttonClickSound; // The sound to play when the button is clicked
    [SerializeField] private AudioSource audioSource;    // The audio source to play the sound

    private void Start()
    {
        // Get the Button component on this GameObject and add a listener to it
        Button button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(PlayButtonClickSound);
        }
        else
        {
            Debug.LogWarning("No Button component found on this GameObject.");
        }
    }

    private void PlayButtonClickSound()
    {
        if (audioSource != null && buttonClickSound != null)
        {
            audioSource.PlayOneShot(buttonClickSound);
        }
        else
        {
            Debug.LogWarning("AudioSource or ButtonClickSound is missing.");
        }
    }
}
