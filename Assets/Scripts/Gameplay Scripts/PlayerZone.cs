using UnityEngine;

public class PlayerZone : MonoBehaviour
{
    public KeyCode hitKey = KeyCode.Space;

    private string targetTag = "Proj"; // Tag for projectiles

    private void Update()
    {
        // Check for player input, then detect if a projectile is in the zone
        if (Input.GetKeyDown(hitKey))
        {
            HandlePlayerInputInZone();
        }
    }

    private void HandlePlayerInputInZone()
    {
        Collider[] hitColliders = Physics.OverlapBox(transform.position, transform.localScale / 2);

        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag(targetTag))
            {
                ProjectileCollisionHandler collisionHandler = hitCollider.GetComponent<ProjectileCollisionHandler>();

                if (collisionHandler != null)
                {
                    collisionHandler.HandlePlayerInput(true); // Assume hit for now
                }
            }
        }
    }
}
