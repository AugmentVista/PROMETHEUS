using UnityEngine;

public class SlowZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Knockback"))
        {
            
            other.attachedRigidbody.velocity *= 0.9f;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Knockback"))
        {
            other.attachedRigidbody.velocity /= 0.9f;
        }
    }
}
