using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : MonoBehaviour
{
    private Transform missZoneTransform;

    private EnemyFire fireScript;

    private MeshRenderer meshRenderer;

    public bool IsAlive = true;


    private void Start()
    {
        fireScript = GetComponent<EnemyFire>();
        meshRenderer = GetComponent<MeshRenderer>();
    }

    public void SetDead()
    {
        IsAlive = false;

        if (fireScript != null)
        {
            fireScript.ToggleFiring(false);
        }

        if (meshRenderer.enabled)
        {
            meshRenderer.enabled = false;
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("MissZone") || collider.CompareTag("Weapon"))
        {
            SetDead();
        }
    }
}
