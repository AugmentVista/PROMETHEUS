using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisposableThrowable : MonoBehaviour
{
    public float health = 5;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("WeaponDestroyer"))
        {
            SelfDestruct();
        }
        if (other.CompareTag("Knockback"))
        {
            health--;
            if (health <= 0)
            {
                SelfDestruct();
            }
        }
    }


    public void SelfDestruct()
    {
        Destroy(gameObject);
    }
}
