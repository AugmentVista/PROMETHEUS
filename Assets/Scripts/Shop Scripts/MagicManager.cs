using UnityEngine;

public class MagicManager : MonoBehaviour
{
    private int previousMagicLevel = 0;
    private int magicLevel;

    [SerializeField] GameObject FirstPortal;
    [SerializeField] GameObject SecondPortal;
    [SerializeField] GameObject ThirdPortal;
    [SerializeField] GameObject FourthPortal;
    void Start()
    {
        FirstPortal.SetActive(false);
        SecondPortal.SetActive(false);
        ThirdPortal.SetActive(false);
        FourthPortal.SetActive(false);
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
        MagicCircleModular magCircle = FirstPortal.GetComponent<MagicCircleModular>();
        MagicCircleModular magCircle2 = SecondPortal.GetComponent<MagicCircleModular>();
        MagicCircleModular magCircle3 = SecondPortal.GetComponent<MagicCircleModular>();
        switch (level)
        {
            case 1:
                FirstPortal.SetActive(true);
                
                magCircle.level = level;
                break;
            case 2:
                SecondPortal.SetActive(true);

                magCircle.level = level;
                magCircle2.level = level;
                break;
            case 3:
                ThirdPortal.SetActive(true);
                magCircle.level = level;
                magCircle2.level = level;
                magCircle3.level = level;
                break;
            case 4:
                FourthPortal.SetActive(true);
                magCircle.level = level;
                magCircle2.level = level;
                magCircle3.level = level;
                break;
        }
    }

    public void LevelUpMagic(float levelUpAmount)
    {
        magicLevel += Mathf.FloorToInt(levelUpAmount);
    }

    public void ResetMagicUpgrade()
    {
        magicLevel = 0;

        MagicCircleModular magCircle = FirstPortal.GetComponent<MagicCircleModular>();
        MagicCircleModular magCircle2 = SecondPortal.GetComponent<MagicCircleModular>();
        MagicCircleModular magCircle3 = SecondPortal.GetComponent<MagicCircleModular>();
        magCircle.level = magicLevel;
        magCircle2.level = magicLevel;
        magCircle3.level = magicLevel;
    }
}
