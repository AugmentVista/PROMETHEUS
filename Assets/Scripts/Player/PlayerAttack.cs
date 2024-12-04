using System.Collections;
using UnityEngine;

public class PlayerAttack : MonoBehaviour // This script is attached to the player weapon
{
    [SerializeField] private KeyCode hitKey = KeyCode.Mouse0;
    private KeyCode blockKey;

    private ProjectileCollisionHandler projectileHandler;
    HammerThrow hammerThrowScript;

    public GameObject rockSmashVFX;
    public GameObject hammerPrefab;
    public GameObject blockerPrefab;
    GameObject hammerInstance;

    private Transform HandLocation;

    public Transform[] Lanes;
    public Transform[] LanesForward1;
    public Transform[] LanesForward2;

    private float checkRadius = 0.5f;

    private bool canCreateBlocker = true;

    float speed = 15f;

    int hammerUpgradesPurchased;

    private void Start()
    {
        hammerThrowScript = hammerPrefab.GetComponent<HammerThrow>();
       
        HandLocation = transform;
    }

    

    private void Update()
    {
        if (Input.GetKeyDown(hitKey))
        {
            Attack();
        }
        if (canCreateBlocker && (Input.GetKeyDown(KeyCode.Alpha1) ||
                                 Input.GetKeyDown(KeyCode.Alpha2) ||
                                 Input.GetKeyDown(KeyCode.Alpha3) ||
                                 Input.GetKeyDown(KeyCode.Alpha4)))
        {
            blockKey = GetPressedAlphaKey();
            StartCoroutine(CreateBlocker(blockKey));
        }
    }

    private void Attack()
    {
        Vector3 randomOffset = new Vector3(0, 0, Random.Range(-1.5f, 1.5f));
        Vector3 spawnPosition = transform.position + randomOffset;

        hammerInstance = Instantiate(hammerPrefab, spawnPosition, Quaternion.identity);

        Rigidbody rb = hammerInstance.GetComponent<Rigidbody>();
        rb.AddForce(Vector3.forward.normalized * speed, ForceMode.Impulse);

        hammerInstance.AddComponent<DisposableThrowable>();
    }

    private IEnumerator CreateBlocker(KeyCode key)
    {
        canCreateBlocker = false;
        int laneIndex = -1;

        switch (key)
        {
            case KeyCode.Alpha1:
                laneIndex = 0;
                break;
            case KeyCode.Alpha2:
                laneIndex = 1;
                break;
            case KeyCode.Alpha3:
                laneIndex = 2;
                break;
            case KeyCode.Alpha4:
                laneIndex = 3;
                break;
        }

        if (laneIndex >= 0 && laneIndex < Lanes.Length)
        {
            Transform spawnPosition = GetAvailableSpawnPosition(laneIndex);
            if (spawnPosition != null)
            {
                Instantiate(blockerPrefab, spawnPosition.position, Quaternion.identity);
            }
            else
            {
                Debug.Log("No available space to spawn blocker in lane " + laneIndex);
            }
        }
        else
        {
            Debug.LogError("Invalid lane index or lane Transform not assigned.");
        }

        yield return new WaitForSeconds(2);
        canCreateBlocker = true;
    }

    //public void UpdateHammer(float amountToEnlarge)
    //{
    //    float convertedValue = 1f + amountToEnlarge / 100;
    //    hammerInstance.transform.localScale *= convertedValue;
    //    hammerInstanceSizeMultiplier = convertedValue;
    //    hammerUpgradesPurchased++;
    //    Debug.Log($"Hammer has grown by {convertedValue} %");

    //}

    private bool IsLaneOccupied(Transform lane)
    {
        if (lane == null) return true; // Treat null lane as occupied

        Collider[] colliders = Physics.OverlapSphere(lane.position, checkRadius);
        foreach (var collider in colliders)
        {
            if (collider.CompareTag("Blocker")) // Assuming blockers have the tag "Blocker"
            {
                return true;
            }
        }

        return false;
    }

    private Transform GetAvailableSpawnPosition(int laneIndex)
    {
        if (!IsLaneOccupied(Lanes[laneIndex]))
        {
            return Lanes[laneIndex];
        }

        if (laneIndex < LanesForward1.Length && !IsLaneOccupied(LanesForward1[laneIndex]))
        {
            return LanesForward1[laneIndex];
        }

        if (laneIndex < LanesForward2.Length && !IsLaneOccupied(LanesForward2[laneIndex]))
        {
            return LanesForward2[laneIndex];
        }

        return null;
    }

    private KeyCode GetPressedAlphaKey()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) return KeyCode.Alpha1;
        if (Input.GetKeyDown(KeyCode.Alpha2)) return KeyCode.Alpha2;
        if (Input.GetKeyDown(KeyCode.Alpha3)) return KeyCode.Alpha3;
        if (Input.GetKeyDown(KeyCode.Alpha4)) return KeyCode.Alpha4;

        return KeyCode.None;
    }

    public void CanPlayerAttackThis(Collider other)
    {
        projectileHandler = other.GetComponent<ProjectileCollisionHandler>();

        if (projectileHandler != null)
        {
            if (other.tag == "Knockback" || other.tag == "TowerBuster")
            {
                projectileHandler.struckByWeapon = true;

                GameObject explosion = Instantiate(rockSmashVFX, other.transform.position, Quaternion.identity);

                explosion.SetActive(true);

                ParticleSystem explosionVFX = explosion.GetComponent<ParticleSystem>();

                if (explosionVFX != null)
                {
                    explosionVFX.Play();
                }
                Destroy(explosion, explosionVFX.main.duration);

            }
            else
            {
                projectileHandler.struckByWeapon = false;
            }
        }
    }
}
