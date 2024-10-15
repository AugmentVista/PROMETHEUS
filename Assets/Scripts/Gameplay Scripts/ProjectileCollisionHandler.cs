using System.Collections;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;

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
    public bool inAttackHitBox;

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
        inAttackHitBox = false;
    }

    void FixedUpdate()
    {
        transform.Rotate(5, 5, 5, Space.Self);
    }

    public void SetSpawner(ProjectileSpawner spawnerReference)
    {
        spawner = spawnerReference;
    }

    private void OnTriggerEnter(Collider other) // checks for collisions based on tags
    {
        if (other.gameObject.CompareTag("PlayerBody")) // this tag needs to be on a small playermodel
        {
            OnPlayerDamaged(true);
            Debug.Log("Player body was struck");
        }
        else if (other.gameObject.CompareTag("MissZone")) // this tag belongs to an object just out of camera view behind player
        {
            OnPlayerDamaged(false); // does nothing
            Debug.Log("Miss zone was struck");
        }
        else if (other.gameObject.CompareTag("Player"))
        {
            Score.score++; // score is unaffected
            Debug.Log("Player was struck");
        }
    }

    public void OnPlayerDamaged(bool didThisHitPlayer)
    {
        if (didThisHitPlayer)
        {
            Score.score--; // score is unaffected
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