using UnityEngine;

public class SlowZone : MonoBehaviour
{
    public float slowMod;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Knockback"))
        {
            
            other.attachedRigidbody.velocity *= slowMod;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Knockback"))
        {
            other.attachedRigidbody.velocity /= slowMod;
        }
    }
}
