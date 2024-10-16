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
    public bool struckByWeapon;

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

    private void OnTriggerEnter(Collider other)
    {
        // Assuming each projectile has a tag that corresponds to its type (e.g., "Stone", "Knockback", "Stun", "Slow", "None")
        switch (other.tag)
        {
            case "Stone":
                HandleProjectileCollision(other, "Stone");
                break;

            case "Knockback":
                HandleProjectileCollision(other, "Knockback");
                break;

            case "Stun":
                HandleProjectileCollision(other, "Stun");
                break;

            case "Slow":
                HandleProjectileCollision(other, "Slow");
                break;

            default:
                Debug.Log("Unknown projectile tag");
                break;
        }
    }

    private void HandleProjectileCollision(Collider other, string projectileType)
    {
        // Determine what was hit (PlayerBody, Player/Weapon, MissZone)
        switch (other.gameObject.tag)
        {
            case "PlayerBody":
                OnPlayerDamaged(true, projectileType); // Player body was struck
                Debug.Log($"{projectileType} hit the PlayerBody");
                break;

            case "Weapon":
                if (struckByWeapon)
                {
                    Score.score++; // Award score for hitting with weapon
                    Debug.Log($"{projectileType} hit the player's weapon and was blocked.");
                    spawner.OnProjectileInactive(gameObject);
                }
                else if (!struckByWeapon)
                {
                    spawner.OnProjectileInactive(gameObject); 
                }
                break;

            case "MissZone":
                OnPlayerDamaged(false, projectileType); // Projectile missed
                Debug.Log($"{projectileType} missed and hit the MissZone.");
                spawner.OnProjectileInactive(gameObject); 
                break;

            default:
                Debug.Log("Unknown hit location");
                break;
        }
    }

    public void OnPlayerDamaged(bool didThisHitPlayer, string projectileType)
    {
        if (didThisHitPlayer)
        {
            switch (projectileType)
            {
                case "Stone":
                    Score.score--; // Reduce score
                    Debug.Log("Stone hit the player. Score reduced.");
                    break;

                case "Knockback":
                    playerMove.WasHit(true, projectileType); // Move player backward
                    Debug.Log("Knockback hit the player. Player moved backward.");
                    break;

                case "Stun":
                    playerMove.WasHit(true, projectileType); // Stun the player
                    Debug.Log("Stun hit the player. Player stunned.");
                    break;

                case "Slow":
                    playerMove.WasHit(true, projectileType); // Slow the player
                    Debug.Log("Slow hit the player. Player movement slowed.");
                    break;

                default:
                    Debug.Log("Unknown projectile type");
                    break;
            }
        }
        else
        {
            Debug.Log($"Projectile missed: {projectileType}");
        }

        // Call spawner to deactivate the projectile
        spawner.OnProjectileInactive(gameObject);
    }
}