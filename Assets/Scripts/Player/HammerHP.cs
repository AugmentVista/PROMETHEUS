using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerHP : MonoBehaviour
{
    public float HP = 30;

    private void TakeDamage(float damage)
    {
        HP -= damage;
        if (HP <= 0f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Knockback")
        {
            BaseProjectile projectile = other.GetComponent<BaseProjectile>();

            if (projectile != null)
            {
                TakeDamage(projectile.knockBackDamage);
            }
        }
    }
}
