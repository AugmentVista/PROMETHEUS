using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlockerHealthBar : MonoBehaviour
{
    [SerializeField] private Blocker blocker;
    [SerializeField] private Image healthBarFill;

    private void Start()
    {
        if (blocker == null)
        {
            blocker = GetComponentInParent<Blocker>();
        }

        if (healthBarFill == null)
        {
            Debug.LogError("HealthBarFill is not assigned!");
        }
    }

    private void Update()
    {
        if (blocker != null && healthBarFill != null)
        {
            healthBarFill.fillAmount = blocker.currentblockerHealth / blocker.blockerMaxHP;
        }
    }
}