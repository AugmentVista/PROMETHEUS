using UnityEngine;

public class ProjectileCollisionHandler : MonoBehaviour
{
    private bool hasCollided = false;
    private float deactivateDelay = 0.2f; // Time to deactivate after a hit
    private float elapsedTime = 0f;

    private ProjectileSpawner spawner; // Reference to the spawner

    private void Update()
    {
        // If projectile has collided, start countdown to deactivation
        if (hasCollided)
        {
            elapsedTime += Time.deltaTime;
            if (elapsedTime >= deactivateDelay)
            {
                DeactivateProjectile();
            }
        }
        else
        {
            // Deactivate the projectile after 10 seconds if no collision happens
            Invoke(nameof(DeactivateProjectile), 10f);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the collision is with the player
        if (collision.gameObject.CompareTag("Player"))
        {
            hasCollided = true;
            HandlePlayerHit();
        }
        else if (collision.gameObject.CompareTag("MissZone"))
        {
            // Handle if the player misses the projectile (Guitar Hero miss equivalent)
            HandleMiss();
            DeactivateProjectile();
        }
    }

    private void HandlePlayerHit()
    {
        // Trigger the effects of a successful hit (like scoring points)
        Debug.Log("Player hit the projectile!");
    }

    private void HandleMiss()
    {
        // Logic for when the player misses the projectile
        Debug.Log("Player missed the projectile!");
    }

    private void DeactivateProjectile()
    {
        // Notify the spawner that the projectile is inactive
        if (spawner != null)
        {
            spawner.OnProjectileInactive();
        }

        gameObject.SetActive(false);
    }

    // Method to set the spawner reference
    public void SetSpawner(ProjectileSpawner spawnerReference)
    {
        spawner = spawnerReference;
    }
}
