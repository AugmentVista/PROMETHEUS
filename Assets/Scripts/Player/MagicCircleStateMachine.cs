using System.Collections.Generic;
using UnityEngine;

public class MagicCircleStateMachine : MonoBehaviour
{
    [SerializeField] GameObject destroyOnContactObject;
    [SerializeField] GameObject hammerPrefab;

    float speed = 15f;

    [SerializeField] Vector3 offset = new Vector3(0, 0, 0);

    public GameObject ThrowBasicHammer()
    {
        return hammerPrefab;
    }

    float RandomExtreme(float min, float max)
    {
        float randomValue = Random.value;
        randomValue = Mathf.Pow(randomValue, 2) * (Random.value < 0.5f ? -1 : 1);
        return Mathf.Lerp(min, max, (randomValue + 1) / 2);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Weapon"))
        {
            MagicCircleModular magicObj = GetComponent<MagicCircleModular>();
            if (magicObj != null)
            {

                if (magicObj.IsTypeMatch("Duplicate"))
                {
                    other.gameObject.SetActive(false);
                    DuplicateHammer(magicObj.level);
                }
                if (magicObj.IsTypeMatch("Fortify"))
                {
                    other.gameObject.SetActive(false);
                    FortifyHammer(magicObj.level);
                }
                if (magicObj.IsTypeMatch("Vitality"))
                {
                    other.gameObject.SetActive(false);
                    BallShooter(magicObj.level * magicObj.level);
                }
                if (magicObj.IsTypeMatch("Mesmerize"))
                {
                    other.gameObject.SetActive(false);
                    MesmerizeShot();
                }
            }
        }
    }

    public void BallShooter(int lifegain)
    {
        Vector3 randomOffset = new Vector3(0, Random.Range(0, 3f), 0);
        Vector3 spawnPosition = transform.position + randomOffset;

        GameObject hammerInstance = Instantiate(hammerPrefab, spawnPosition, Quaternion.identity);

        Rigidbody rb = hammerInstance.GetComponent<Rigidbody>();
        rb.AddForce(Vector3.forward.normalized * 30f, ForceMode.Impulse);
    }

    public void FortifyHammer(int sizeMultiplier)
    {
        Vector3 randomOffset = new Vector3(0, 0, Random.Range(2f, 5f));
        Vector3 spawnPosition = transform.position + randomOffset;

        GameObject hammerInstance = Instantiate(hammerPrefab, spawnPosition, Quaternion.identity);

        float scaleMultiplier = 1f + (sizeMultiplier * 1.1f);
        Vector3 newScale = hammerPrefab.transform.localScale * scaleMultiplier;
        hammerInstance.transform.localScale = newScale;

        Rigidbody rb = hammerInstance.GetComponent<Rigidbody>();
        rb.AddForce(Vector3.forward.normalized * speed, ForceMode.Impulse);

        hammerInstance.AddComponent<DisposableThrowable>();
        hammerInstance.GetComponent<DisposableThrowable>().health = 10f;

        Debug.Log($"Hammer created with scale multiplier: {scaleMultiplier}, Final scale: {newScale}");
    }

    public void DuplicateHammer(int amountToCreate)
    {
        for (int i = 0; i < amountToCreate + 1; i++)
        {
            Vector3 randomOffset = new Vector3(RandomExtreme(-2f, 2f), RandomExtreme(-0.5f, 3f), RandomExtreme(-2f, 10f));
            Vector3 spawnPosition = transform.position + randomOffset;

            GameObject hammerInstance = Instantiate(hammerPrefab, spawnPosition, Quaternion.identity);

            Rigidbody rb = hammerInstance.GetComponent<Rigidbody>();
            rb.AddForce(Vector3.forward.normalized * (speed + Random.Range(2f, 10f)), ForceMode.Impulse);

            hammerInstance.AddComponent<DisposableThrowable>();
        }
    }

    public void MesmerizeShot()
    {
        Vector3 spawnPosition = transform.position;
     
        GameObject hammerInstance = Instantiate(hammerPrefab, spawnPosition, Quaternion.identity);

        Rigidbody rb = hammerInstance.GetComponent<Rigidbody>();
        rb.AddForce(Vector3.forward.normalized * speed, ForceMode.Impulse);
    }


}
