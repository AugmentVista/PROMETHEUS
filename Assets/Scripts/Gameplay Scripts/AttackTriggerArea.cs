using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTriggerArea : MonoBehaviour
{
    public GameObject Projectile; // Uncommented to instantiate projectiles later on
    private string targetTag = "Proj";
    private float upAmount = 2;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other);
        if (other.CompareTag(targetTag))
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb != null)
            {
                Vector3 reversedVelocity = rb.velocity * -1; // Reverse the velocity
                rb.velocity = reversedVelocity; // Apply the reversed velocity
            }

            Vector3 newPosition = new Vector3(other.transform.position.x, other.transform.position.y + upAmount, other.transform.position.z);
            GameObject newShot = Instantiate(Projectile, newPosition, other.transform.rotation);
            upAmount++;
            // Set the new shot's velocity if needed
            Rigidbody newRb = newShot.GetComponent<Rigidbody>();
            if (newRb != null)
            {
                newRb.velocity = rb.velocity; // Set the same velocity as the original, if desired
            }
        }
    }
}
