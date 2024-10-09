using System.Collections;
using UnityEngine;

public class ProjectileCollisionHandler : MonoBehaviour
{
    /// <summary>
    /// This class is attached to projectiles when they are instantiated. 
    /// It handles only what the projectile needs to know.
    /// It knows if it hits the Player or if it hits the Miss Zone.
    /// It can reduce your score if you get hit.
    /// </summary>
   
    public ScoreKeeper Score;
    private PlayerMovement playerMove;
    private ProjectileSpawner spawner; // Reference to the spawner

    public bool reusedProjectile = false; // Track whether this projectile is reused
    public bool inAttackHitBox = false;

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

    private void OnCollisionEnter(Collision collision) // checks for collisions based on tags
    {
        if (collision.gameObject.CompareTag("PlayerBody")) // this tag needs to be on a small playermodel
        {
            OnPlayerDamaged(true);
        }
        else if (collision.gameObject.CompareTag("MissZone")) // this tag belongs to an object just out of camera view behind player
        {
            OnPlayerDamaged(false); // does nothing
        }
    }

    public void OnPlayerDamaged(bool didThisHitPlayer)
    {
        if (didThisHitPlayer)
        {
            Score.score--;
            playerMove.WasHit(true); // Sends player back a space
        }
        else
        {
            playerMove.WasHit(false); // does nothing
        }

        // Call spawner to deactivate the projectile
        spawner.OnProjectileInactive(gameObject);
    }
}