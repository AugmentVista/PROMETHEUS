using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class HammerThrow : MonoBehaviour
{
    [SerializeField] GameObject destroyOnContactObject;
    [SerializeField] GameObject hammerPrefab;

    float speed = 10f;

    [SerializeField] Vector3 offset = new Vector3(0, 0, 0);

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MagicCircle"))
        {
            MagicCircleModular magicObj = other.GetComponent<MagicCircleModular>();
            if (magicObj != null)
            {
                Debug.Log(other.name);

                if (magicObj.IsTypeMatch("Duplicate"))
                {
                    DuplicateHammer(magicObj.level);
                }
                if (magicObj.IsTypeMatch("Fortify"))
                {
                    FortifyHammer(magicObj.level);
                }
            }
        }
    }

    public void FortifyHammer(int size)
    {
        Vector3 randomOffset = new Vector3(0, 0, Random.Range(-1.5f, 1.5f));
        Vector3 spawnPosition = transform.position + randomOffset;

        GameObject hammerInstance = Instantiate(hammerPrefab, spawnPosition, Quaternion.identity);
        hammerInstance.transform.localScale *= size;

        Rigidbody rb = hammerInstance.GetComponent<Rigidbody>();
        rb.AddForce(Vector3.forward.normalized * speed, ForceMode.Impulse);

        Blocker HP = hammerInstance.GetComponent<Blocker>();
        
        HP.blockerMaxHP *= size;
        hammerInstance.AddComponent<DisposableThrowable>();
    }

    public void DuplicateHammer(int amountToCreate)
    {
        for (int i = 0; i < amountToCreate; i++)
        {
            Vector3 randomOffset = new Vector3(0, 0, Random.Range(-1.5f, 1.5f));
            Vector3 spawnPosition = transform.position + new Vector3(-3 + i, 0, 0) + randomOffset;

            GameObject hammerInstance = Instantiate(hammerPrefab, spawnPosition, Quaternion.identity);

            // Get Rigidbody and add force with slight directional variation
            Rigidbody rb = hammerInstance.GetComponent<Rigidbody>();
            Vector3 randomDirection = Vector3.forward + new Vector3(Random.Range(-0.1f, 0.1f), Random.Range(-0.05f, 0.05f), 0);
            rb.AddForce(randomDirection.normalized * speed, ForceMode.Impulse);

            // Add the DisposableThrowable component
            hammerInstance.AddComponent<DisposableThrowable>();
        }
    }

}
