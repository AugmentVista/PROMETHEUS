using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TimerController : MonoBehaviour
{
    public Image[] NumbersImages;

    public Image Ones;
    public Image Tens;
    public Image Hundreds;
    public Image fillImage;

    public float duration = 10f;
    public float elapsedTime = 0f;

    private int lastDisplayedTime = -1;
    int remainingTime;

    public static bool TimerOver = false;

    void Start()
    {
        fillImage.fillAmount = 1.0f;
        SetTimerUINumbers(Mathf.CeilToInt(duration - elapsedTime));
    }

    void Update()
    {
        elapsedTime += Time.deltaTime;

        float fillValue = Mathf.Lerp(1.0f, 0.0f, elapsedTime / duration);
        UpdateFillAmount(fillValue);

        remainingTime = Mathf.CeilToInt(duration - elapsedTime);
        Debug.Log(remainingTime.ToString());

        if (remainingTime != lastDisplayedTime && remainingTime >= 0)
        {
            SetTimerUINumbers(remainingTime);
            lastDisplayedTime = remainingTime;
        }


        if (elapsedTime > duration)
        { 
            TimerOver = true;
        }
    }

    private void SetTimerUINumbers(int time)
    {
        int ones = time % 10;
        int tens = (time / 10) % 10;
        int hundreds = (time / 100) % 10;

        Ones.sprite = NumbersImages[ones].sprite; // Access the sprite property to set the image
        Tens.sprite = NumbersImages[tens].sprite;
        Hundreds.sprite = NumbersImages[hundreds].sprite;
    }

    public void UpdateFillAmount(float value)
    {
        if (fillImage != null)
        {
            // Ensure the fill value stays between 0 and 1
            fillImage.fillAmount = Mathf.Clamp(value, 0f, 1f);
        }
    }

    public void AddTime(float timeToAdd)
    {
        elapsedTime =- timeToAdd;
    }
}
