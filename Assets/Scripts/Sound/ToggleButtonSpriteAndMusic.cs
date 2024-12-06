using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ToggleButtonSpriteAndMusic : MonoBehaviour
{
    public AudioSource musicSource;

    public Image displayedImage;
    public Image buttonImagePlaying;
    public Image buttonImagePaused;

    private bool isMusicPlaying = true;

    public void PlayPause()
    {
        if (isMusicPlaying) 
        { 
            isMusicPlaying = false;
            musicSource.Pause();
        }
        else if (!isMusicPlaying) 
        { 
            isMusicPlaying = true;
            musicSource.Play();
        }
    }

    private void Update()
    {
        if (isMusicPlaying)
        {
            displayedImage.sprite = buttonImagePlaying.sprite;
        }
        else if (!isMusicPlaying)
        {
            displayedImage.sprite = buttonImagePaused.sprite;
        }
    }
}
