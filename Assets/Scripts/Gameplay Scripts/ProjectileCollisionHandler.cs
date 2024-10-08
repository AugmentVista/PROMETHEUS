using System.Collections;
using UnityEngine;

public class ProjectileCollisionHandler : MonoBehaviour
{
    public ScoreKeeper Score;
    public bool reusedProjectile = false; // Track whether this projectile is reused
    private PlayerMovement playerMove;

    private float deactivateDelay = 0.5f; // Time to deactivate after a hit
    private ProjectileSpawner spawner; // Reference to the spawner

    public enum ShotType
    { 
        Knockback,
        Slow
    }

    private void Start()
    {
        Score = FindAnyObjectByType<ScoreKeeper>();
        spawner = FindObjectOfType<ProjectileSpawner>();
        playerMove = FindObjectOfType<PlayerMovement>();
    }

    public void SetSpawner(ProjectileSpawner spawnerReference)
    {
        spawner = spawnerReference;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(DeactivateAfterDelay(true)); // Player hit
        }
        else if (collision.gameObject.CompareTag("MissZone"))
        {
            StartCoroutine(DeactivateAfterDelay(false)); // Player missed
        }
    }

    private IEnumerator DeactivateAfterDelay(bool didPlayerHitThis)
    {
        yield return new WaitForSeconds(deactivateDelay);
        HandlePlayerInput(didPlayerHitThis);
    }

    public void HandlePlayerInput(bool didPlayerHitThis)
    {
        if (didPlayerHitThis)
        {
            Score.score++;
            playerMove.WasHit(false);
        }
        else
        {
            Score.score--;
            playerMove.WasHit(true);
        }

        // Call spawner to deactivate the projectile
        spawner.OnProjectileInactive(gameObject);
    }
}