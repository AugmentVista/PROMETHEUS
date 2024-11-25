using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicManager : MonoBehaviour
{
    private int previousMagicLevel = 0; // Tracks the last processed level
    private int magicLevel;

    [SerializeField] GameObject FirstPortal;
    [SerializeField] GameObject SecondPortal;
    [SerializeField] GameObject ThirdPortal;
    //[SerializeField] GameObject FourthPortal;


    void Start()
    {
        FirstPortal.SetActive(false);
        SecondPortal.SetActive(false);
        ThirdPortal.SetActive(false);
        //FourthPortal.SetActive(false);
    }


    private void Update()
    {
        if (magicLevel != previousMagicLevel)
        {
            ActivatePortal(magicLevel);
            previousMagicLevel = magicLevel;
        }
    }

    private void ActivatePortal(int level)
    {
        switch (level)
        {
            case 1:
                FirstPortal.SetActive(true);
                MagicCircleModular magCircle = FirstPortal.GetComponent<MagicCircleModular>();
                magCircle.scriptableMagic.level = level;
                break;
            case 2:
                SecondPortal.SetActive(true);
                MagicCircleModular magCircle2 = SecondPortal.GetComponent<MagicCircleModular>();
                magCircle2.scriptableMagic.level = level;
                break;
            case 3:
                ThirdPortal.SetActive(true);
                MagicCircleModular magCircle3 = SecondPortal.GetComponent<MagicCircleModular>();
                magCircle3.scriptableMagic.level = level;
                break;
            // Add additional cases if needed
            default:
                Debug.LogWarning("Unhandled magic level: " + level);
                break;
        }
    }

    public void LevelUpMagic(float levelUpAmount)
    {
        magicLevel += Mathf.FloorToInt(levelUpAmount); // Ensure consistent integer behavior
    }


}
