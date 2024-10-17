using UnityEngine;
using UnityEngine.UI;

public class TimerController : MonoBehaviour
{
    public Image fillImage;

    public float duration = 10f;

    public float elapsedTime = 0f;

    public float fillValue;

    void Start()
    {
        fillImage.fillAmount = 1.0f;
    }

    void Update()
    {
        elapsedTime += Time.deltaTime;

         fillValue = Mathf.Lerp(1.0f, 0.0f, elapsedTime / duration);

        UpdateFillAmount(fillValue);
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
