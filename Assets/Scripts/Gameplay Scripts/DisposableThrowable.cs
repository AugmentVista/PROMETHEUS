using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisposableThrowable : MonoBehaviour
{
    public float health = 20;

    private void OnTriggerEnter(Collider other)
    {
        //if (other.CompareTag("KnockBack"))
        //{
        //    BaseProjectile baseProj = other.GetComponent<BaseProjectile>();
        //    if (baseProj != null)
        //    {
        //        health -= baseProj.knockBackDamage;
        //        if (health > 0)
        //        {
        //            SelfDestruct();
        //        }
        //    }
        //}
        if (other.CompareTag("WeaponDestroyer"))
        {
            SelfDestruct();
        }
    }

    public void SelfDestruct()
    {
        Destroy(gameObject);
    }
}
