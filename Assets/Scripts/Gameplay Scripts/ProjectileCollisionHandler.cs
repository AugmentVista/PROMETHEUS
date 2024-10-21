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
    private PlayerAttackHitBox playerAttack;
    private BaseProjectile Base;
    private string[] Type;

    public bool reusedProjectile = false; 
    public bool struckByWeapon;

   

    private Collider projectileCollider;

    private void Start()
    {
        Base = GetComponent<BaseProjectile>();
        Score = FindAnyObjectByType<ScoreKeeper>();
        spawner = FindObjectOfType<ProjectileSpawner>();
        playerMove = FindObjectOfType<PlayerMovement>();
        playerAttack = FindObjectOfType<PlayerAttackHitBox>();
        projectileCollider = GetComponent<Collider>();

    }

    public void SetSpawner(ProjectileSpawner spawnerReference)
    {
        spawner = spawnerReference;
    }
    public Collider GetProjectileCollider()
    {
        return projectileCollider;
    }


    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"OnTriggerEnter called with: {other.gameObject.tag}");
        switch (other.gameObject.tag)
        {
            case "Weapon":
                playerAttack.CanPlayerAttackThis(projectileCollider);
                Debug.Log("Called CanPlayerAttackThis from OnTriggerEnter");
                if (!struckByWeapon) { return; }
                else
                { 
                    HandleProjectileCollision(other, "Weapon");
                }
                break;

            case "PlayerBody":
                if (!struckByWeapon)
                { 
                HandleProjectileCollision(other, "PlayerBody");
                }
                break;  

            case "MissZone":
                HandleProjectileCollision(other, "MissZone");
                break;

            default:
                Debug.Log("Unknown projectile tag");
                break;
        }
    }

    private void HandleProjectileCollision(Collider other, string projectileType)
    {
        // Determine what was hit (PlayerBody, Weapon, MissZone)
        switch (other.gameObject.tag) // the tag of this object
        {
            case "PlayerBody":
                OnPlayerDamaged(true, gameObject.tag); 

                Debug.Log($"{gameObject.tag} hit the PlayerBody");
                Score.score--;
                DisableColliderForPooling();
                break;

            case "Weapon":
                if (struckByWeapon)
                {
                    Score.score++;
                    
                    Debug.Log($"{projectileType} hit the player's weapon and was blocked.");
                    DisableColliderForPooling();
                }
                else if (!struckByWeapon)
                {
                    DisableColliderForPooling(); 
                }
                break;

            case "MissZone":
                OnPlayerDamaged(false, gameObject.tag);

                Debug.Log($"{gameObject.tag} missed and hit the MissZone.");

                DisableColliderForPooling(); 
                break;

            default:
                Debug.Log("Unknown hit location");
                DisableColliderForPooling();
                break;
        }
    }

    public void OnPlayerDamaged(bool didThisHitPlayer, string projectileType)
    {
        if (didThisHitPlayer)
        {
            Debug.Log($"Player hit {projectileType} !!!!!");
            switch (projectileType)
            {
                case "Stone":
                    Score.score--; // Reduce score
                    PlayerHealthSystem.TakeDamage(Base.stunDamage);
                    playerMove.WasHit(true, projectileType);
                    Debug.Log("Stone hit the player. Score reduced.");
                    break;

                case "Knockback":
                    PlayerHealthSystem.TakeDamage(Base.knockBackDamage);
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
                    Debug.Log("Player was not hit");
                    break;
            }
        }
        else
        {
            Debug.Log($"Projectile missed: {projectileType}");
        }

        // Call spawner to deactivate the projectile
        DisableColliderForPooling();
    }

    private void DisableColliderForPooling()
    {
        // Disable the collider to prevent further interactions
        GetComponent<Collider>().enabled = false;
        GetComponent<Renderer>().enabled = false;

        // Call the spawner to handle the pooling logic if needed
        spawner.OnProjectileInactive(gameObject);
    }
}