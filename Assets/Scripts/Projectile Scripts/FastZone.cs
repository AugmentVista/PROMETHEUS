using UnityEngine;

public class FastZone : MonoBehaviour
{
    public EnemyProjectileManager enemyProjectileManager;
    float fastMod; 

    private void Start()
    {
        fastMod = 1 + (enemyProjectileManager.localWaveCount / 10);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Knockback"))
        {
            other.attachedRigidbody.velocity *= fastMod;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Knockback"))
        {
            other.attachedRigidbody.velocity /= fastMod;
        }
    }
}
