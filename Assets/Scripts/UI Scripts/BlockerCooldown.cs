using TMPro;
using UnityEngine;

public class BlockerCooldown : MonoBehaviour
{
    bool isGameActive = true;

    private KeyCode blockKey;

    private float elapsedTime = 0f;

    float blockerCooldown = 2f;

    float blockerCountdown = 0f;

    public TextMeshProUGUI blockerUIReadyText;


    private void CheckBlockerSpawnCooldown()
    {
        if (GlobalSettings.globalPauseOverride)
        {
            isGameActive = false;
        }
        else
        {
            isGameActive = true;
            elapsedTime += Time.deltaTime;
        }
    }

    private void Update()
    {
        CheckBlockerSpawnCooldown();
        if (CanCreateBlocker())
        {
            blockerUIReadyText.text = "Blocker Ready";
            if (Input.GetKeyDown(KeyCode.Alpha1) ||
                                 Input.GetKeyDown(KeyCode.Alpha2) ||
                                 Input.GetKeyDown(KeyCode.Alpha3) ||
                                 Input.GetKeyDown(KeyCode.Alpha4))
            {
                blockKey = GetPressedAlphaKey();
                elapsedTime = 0f;
            }
        }
        else
        {
            blockerUIReadyText.text = $"Cooldown {blockerCountdown.ToString("F2")}";
        }
    }


    public bool CanCreateBlocker() // returns true if we can make a blocker
    {
        if (elapsedTime < blockerCooldown && isGameActive) // if time passed is less than time needed, display time
        {
            blockerCountdown = blockerCooldown - elapsedTime;
            if (blockerCountdown < 0)
            {
                blockerCountdown = 0;
            }
            return false;
        }
        else
        { 
            return true;
        }
    }


    private KeyCode GetPressedAlphaKey()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) return KeyCode.Alpha1;
        if (Input.GetKeyDown(KeyCode.Alpha2)) return KeyCode.Alpha2;
        if (Input.GetKeyDown(KeyCode.Alpha3)) return KeyCode.Alpha3;
        if (Input.GetKeyDown(KeyCode.Alpha4)) return KeyCode.Alpha4;

        return KeyCode.None;
    }

}