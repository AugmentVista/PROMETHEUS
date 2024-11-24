using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShowHideHitbox : MonoBehaviour
{
    [SerializeField] private PlayerAttack hitBox;
    [SerializeField] private Slider slider;
    [SerializeField] private GameObject hitBoxChild;
    public void DisplayHitBoxUI()
    {
        PlayerAttack hitBox = FindAnyObjectByType<PlayerAttack>();
        Slider slider = gameObject.GetComponent<Slider>();
        if (hitBox != null)
        {
            GameObject hitBoxChild = hitBox.transform.Find("Hit Box Visual")?.gameObject;

            if (hitBoxChild != null)
            {
                if (slider != null)
                {
                    if (slider.value > 0.5f)
                    {
                        hitBoxChild.SetActive(true);
                    }
                    if (slider.value < 0.5f)
                    {
                        hitBoxChild.SetActive(false);
                    }
                }
            }
        }
    }
}
