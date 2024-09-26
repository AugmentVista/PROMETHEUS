using UnityEngine;

public class ProjectileCollisionHandler : MonoBehaviour
{
    private bool hasCollided = false;
    private float deactivateDelay = 0.2f; // Time to deactivate after a hit
    private float elapsedTime = 0f;

    private ProjectileSpawner spawner; // Reference to the spawner
    private PlayerZone playerZone;

    private void Start()
    {
        ProjectileSpawner spawner = FindObjectOfType<ProjectileSpawner>();
        PlayerZone playerZone = FindObjectOfType<PlayerZone>();
    }
    public void SetSpawner(ProjectileSpawner spawnerReference)
    {
        spawner = spawnerReference;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision confirmed");

        if (collision.gameObject.CompareTag("Player"))
        {
            hasCollided = true;
        }
        else if (collision.gameObject.CompareTag("MissZone"))
        {
            hasCollided = true;
        }
    }

    private void CheckCollision()
    {
        if (hasCollided)
        {
            Debug.Log("Collided");
            elapsedTime += Time.deltaTime;
            if (elapsedTime >= deactivateDelay)
            {
                DeactivateProjectile(gameObject);
            }
        }
        else // Handles if the projectile if misses entirely by some strange means and deactivates it
        {
            //Invoke(nameof(DeactivateProjectile), 5.0f);
            //Debug.Log("Something went wrong");
        }
    }

    private void Update()
    {
        CheckCollision();
    }


    public void HandlePlayerInput(bool didPlayerHitThis)
    {
        if (didPlayerHitThis)
        {
            Debug.Log("Player hit the projectile!");
        }
        else if (!didPlayerHitThis)
        {
            Debug.Log("Player missed the projectile!");
        }
        DeactivateProjectile(gameObject);
    }

    public void DeactivateProjectile(GameObject obj)
    {
        if (spawner != null)
        {
            spawner.OnProjectileInactive();
        }

        obj.SetActive(false); // Deactivate the projectile for pooling
    }
}
