using System.Collections.Generic;
using UnityEngine;

public class MagicCircleStateMachine : MonoBehaviour
{
    [SerializeField] GameObject destroyOnContactObject;
    [SerializeField] GameObject hammerPrefab;

    float speed = 10f;

    [SerializeField] Vector3 offset = new Vector3(0, 0, 0);

    public GameObject ThrowBasicHammer()
    {
        return hammerPrefab;
    }

    [SerializeField] List<GameObject> polymorphObjects = new List<GameObject>();




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
                    EnduringHammer(magicObj.level * magicObj.level);
                }
                if (magicObj.IsTypeMatch("Polymorph"))
                {
                    other.gameObject.SetActive(false);
                    PolymorphHammer();
                }
            }
        }
    }

    public void EnduringHammer(int lifegain)
    {
        Vector3 randomOffset = new Vector3(0, 0, Random.Range(-1.5f, 1.5f));
        Vector3 spawnPosition = transform.position + randomOffset;

        GameObject hammerInstance = Instantiate(hammerPrefab, spawnPosition, Quaternion.identity);

        Rigidbody rb = hammerInstance.GetComponent<Rigidbody>();
        rb.AddForce(Vector3.forward.normalized * speed, ForceMode.Impulse);

        CityHealthSystem cityHP = FindObjectOfType<CityHealthSystem>();
        PlayerHealthSystem playerHealthSystem = FindObjectOfType<PlayerHealthSystem>();
        Debug.Log(cityHP.currentHealth + "CITY HP BEFORE");
        playerHealthSystem.Heal(lifegain);
        cityHP.Heal(lifegain);
        hammerInstance.AddComponent<DisposableThrowable>();
        Debug.Log(cityHP.currentHealth + "CITY HP AFTER");
    }

    public void FortifyHammer(int sizeMultiplier)
    {
        Vector3 randomOffset = new Vector3(0, 0, Random.Range(-1.5f, 1.5f));
        Vector3 spawnPosition = transform.position + randomOffset;

        GameObject hammerInstance = Instantiate(hammerPrefab, spawnPosition, Quaternion.identity);

        float scaleMultiplier = 1f + (sizeMultiplier * 1.2f);
        Vector3 newScale = hammerPrefab.transform.localScale * scaleMultiplier;
        hammerInstance.transform.localScale = newScale;

        Rigidbody rb = hammerInstance.GetComponent<Rigidbody>();
        rb.AddForce(Vector3.forward.normalized * speed, ForceMode.Impulse);

        hammerInstance.AddComponent<DisposableThrowable>();

        Debug.Log($"Hammer created with scale multiplier: {scaleMultiplier}, Final scale: {newScale}");
    }

    public void DuplicateHammer(int amountToCreate)
    {
        for (int i = 0; i < amountToCreate+1; i++)
        {
            Vector3 randomOffset = new Vector3(0, 0, Random.Range(-1.5f, 1.5f));
            Vector3 spawnPosition = transform.position + new Vector3(0, 0, 0) + randomOffset;

            GameObject hammerInstance = Instantiate(hammerPrefab, spawnPosition, Quaternion.identity);

            Rigidbody rb = hammerInstance.GetComponent<Rigidbody>();
            Vector3 randomDirection = Vector3.forward + new Vector3(Random.Range(-0.1f, 0.1f), Random.Range(-0.05f, 0.05f), 0);
            rb.AddForce(randomDirection.normalized * speed, ForceMode.Impulse);

            hammerInstance.AddComponent<DisposableThrowable>();
        }
    }

    public void PolymorphHammer()
    {
        GameObject randomPrefab;
        Vector3 randomOffset = new Vector3(0, 0, Random.Range(-3f, 3f));
        Vector3 spawnPosition = transform.position + randomOffset;

        if (polymorphObjects.Count > 0)
        {
            int randomPolymorphObject = Random.Range(0, polymorphObjects.Count);
            randomPrefab = polymorphObjects[randomPolymorphObject];
        }
        else
        { 
            randomPrefab = null;
            return;
        }
        GameObject hammerInstance = Instantiate(randomPrefab, spawnPosition, Quaternion.identity);

        Rigidbody rb = hammerInstance.GetComponent<Rigidbody>();
        rb.AddForce(Vector3.forward.normalized * speed * Random.Range(1.1f, 2f), ForceMode.Impulse);

        hammerInstance.AddComponent<DisposableThrowable>();
    }


}
