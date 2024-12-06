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

    private CurrencyKeeper Score;
    private PlayerSideToSide playerMove;
    private PlayerAttack playerAttack;
    private BaseProjectile Base;
    private string[] Type;
    public bool struckByWeapon;

    private Collider projectileCollider;

    private void Start()
    {
        Base = GetComponent<BaseProjectile>();
        Score = FindAnyObjectByType<CurrencyKeeper>();
        playerMove = FindObjectOfType<PlayerSideToSide>();
        playerAttack = FindObjectOfType<PlayerAttack>();
        projectileCollider = GetComponent<Collider>();
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
        }
    }

    private void HandleProjectileCollision(Collider other, string projectileType)
    {
        switch (other.gameObject.tag)
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
                        Score.drachma += Base.value;
                    }

                    DisableColliderForPooling();
                }
                else
                {
                    DisableColliderForPooling(); 
                }
                break;

            case "MissZone":
                OnPlayerDamaged(false, gameObject.tag);

                DisableColliderForPooling(); 
                break;

            case "Blocker":
                Blocker blocker = other.GetComponent<Blocker>();
                blocker.BlockerTakeDamage(Base.knockBackDamage);

                DisableColliderForPooling();
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
                    playerMove.WasHit(true, projectileType);
                    break;

                case "Knockback":
                    PlayerHealthSystem.TakeDamage(Base.knockBackDamage);
                    playerMove.WasHit(true, projectileType);
                    break;

                case "TowerBuster":
                    playerMove.WasHit(true, projectileType);
                    break;

                case "Slow":
                    playerMove.WasHit(true, projectileType);
                    break;
            }
        }
        DisableColliderForPooling();
    }

    private void DisableColliderForPooling()
    {
        GetComponent<Collider>().enabled = false;
        GetComponent<Renderer>().enabled = false;
        Destroy(gameObject, 5f);
    }
}