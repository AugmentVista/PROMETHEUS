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

    private ScoreKeeper Score;
    private PlayerSideToSide playerMove;
    private EnemyProjectileManager spawner; // Reference to the spawner
    private PlayerAttack playerAttack;
    private BaseProjectile Base;
    private string[] Type;

    public bool reusedProjectile = false; 
    public bool struckByWeapon;

    private Collider projectileCollider;

    private void Start()
    {
        Base = GetComponent<BaseProjectile>();
        Score = FindAnyObjectByType<ScoreKeeper>();
        spawner = FindObjectOfType<EnemyProjectileManager>();
        playerMove = FindObjectOfType<PlayerSideToSide>();
        playerAttack = FindObjectOfType<PlayerAttack>();
        projectileCollider = GetComponent<Collider>();
    }

    public void SetSpawner(EnemyProjectileManager spawnerReference)
    {
        spawner = spawnerReference;
    }

    public Collider GetProjectileCollider()
    {
        return projectileCollider;
    }


    private void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.tag)
        {
            case "Weapon":
                playerAttack.CanPlayerAttackThis(projectileCollider);
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

            case "Blocker":
                HandleProjectileCollision(other, "Blocker");
                break;

            default:
               // Debug.Log("Unknown projectile tag");
                break;
        }
    }

    private void HandleProjectileCollision(Collider other, string projectileType)
    {
        switch (other.gameObject.tag) // the tag of this object
        {
            case "PlayerBody":
                OnPlayerDamaged(true, gameObject.tag); 

                DisableColliderForPooling();
                break;

            case "Weapon":
                if (struckByWeapon)
                {
                    if (Score != null) 
                    {
                        Score.score += Base.value;
                        Score.drachma += Base.value;
                    }

                    DisableColliderForPooling();
                }
                else if (!struckByWeapon)
                {
                    DisableColliderForPooling(); 
                }
                break;

            case "MissZone":
                OnPlayerDamaged(false, gameObject.tag);

                //OnTowerDamaged(true, gameObject.tag)

                DisableColliderForPooling(); 
                break;

            case "Blocker":
                Blocker blocker = other.GetComponent<Blocker>();
                blocker.BlockerTakeDamage(Base.knockBackDamage);

                DisableColliderForPooling();
                break;

            default:
                DisableColliderForPooling();
                break;
        }
    }

    public void OnPlayerDamaged(bool didThisHitPlayer, string projectileType)
    {
        if (didThisHitPlayer)
        {
            //Debug.Log($"Player hit {projectileType} !!!!!");
            switch (projectileType)
            {
                case "Stone":// Reduce score
                    playerMove.WasHit(true, projectileType);
                    break;

                case "Knockback":
                    PlayerHealthSystem.TakeDamage(Base.knockBackDamage);
                    playerMove.WasHit(true, projectileType); // Move player backward
                    break;

                case "TowerBuster":
                    playerMove.WasHit(true, projectileType); // Stun the player
                    break;

                case "Slow":
                    playerMove.WasHit(true, projectileType); // Slow the player
                    break;

                default:
                    //Debug.Log("Player was not hit");
                    break;
            }
        }
        DisableColliderForPooling();
    }

    private void DisableColliderForPooling()
    {
        GetComponent<Collider>().enabled = false;
        GetComponent<Renderer>().enabled = false;

        spawner.ReturnProjectile(gameObject);
    }
}