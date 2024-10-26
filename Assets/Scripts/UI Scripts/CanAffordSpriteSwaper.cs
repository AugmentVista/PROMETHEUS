using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CanAffordSpriteSwapper : MonoBehaviour
{
    public Image ONES;
    public Image TENS;
    public Image HUNDREDS;

    public bool isAffordable; // true = green, false = red

    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
    }

    // Method to set colors when highlighted
    public void OnHighlight()
    {
        Color targetColor = isAffordable ? Color.green : Color.red;
        SetImageColors(targetColor);
    }

    // Method to reset colors when not highlighted
    public void OnUnhighlight()
    {
        SetImageColors(Color.white);
    }

    private void SetImageColors(Color color)
    {
        ONES.color = color;
        TENS.color = color;
        HUNDREDS.color = color;
    }
}
