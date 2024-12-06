using System.Collections;
using UnityEngine;

public class DisposableThrowable : MonoBehaviour
{
    public float health = 2;


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
