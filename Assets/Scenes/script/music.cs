using UnityEngine;

public class ButtonSoundManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip playClip;
    public AudioClip quitClip;

    public void PlayPlaySound()
    {
        audioSource.clip = playClip;
        audioSource.Play();
    }

    public void PlayQuitSound()
    {
        audioSource.clip = quitClip;
        audioSource.Play();
    }
}
