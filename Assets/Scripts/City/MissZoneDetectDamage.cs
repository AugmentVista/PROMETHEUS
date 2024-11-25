using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissZoneDetectDamage : MonoBehaviour
{
    CityHealthSystem cityHP;

    private void Start()
    {
        cityHP = FindObjectOfType<CityHealthSystem>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (cityHP != null && other.tag == "Knockback")
        {
            BaseProjectile projectile = other.GetComponent<BaseProjectile>();

            if (projectile != null)
            { 
                cityHP.TakeDamage(projectile.knockBackDamage);
            }
        }
    }
}
