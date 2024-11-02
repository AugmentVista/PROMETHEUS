using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ToggleButtonSpriteAndMusic : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public Sprite defaultSprite;
    public Sprite hoverSprite;
    public Sprite clickSprite;

    public AudioSource musicSource;

    private Image buttonImage;
    private bool isMusicPlaying = true; // Assuming music is playing by default

    private void Awake()
    {
        buttonImage = GetComponent<Image>();
        buttonImage.sprite = defaultSprite;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!isMusicPlaying)
        {
            buttonImage.sprite = clickSprite; // Keep clicked sprite when hovering if music is off
        }
        else
        {
            buttonImage.sprite = hoverSprite;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!isMusicPlaying)
        {
            buttonImage.sprite = clickSprite; // Keep clicked sprite when not hovering if music is off
        }
        else
        {
            buttonImage.sprite = defaultSprite;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        isMusicPlaying = !isMusicPlaying; // Toggle music state

        if (isMusicPlaying)
        {
            buttonImage.sprite = defaultSprite;
            musicSource.UnPause();
        }
        else
        {
            buttonImage.sprite = clickSprite;
            musicSource.Pause();
        }
    }
}
