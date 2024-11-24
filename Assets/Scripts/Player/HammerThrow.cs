using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerThrow : MonoBehaviour
{
    [SerializeField] GameObject destroyOnContactObject;
    [SerializeField] GameObject hammerPrefab;

    [SerializeField] Transform releasePosition;

    [SerializeField] float speed = 1000f;

    [SerializeField] Vector3 offset = new Vector3(2, 0, 0);

    private int rangeUpgrades = 1;
    private int upgradeAmount = 10;
    private int contactObjectDistance = 1;

    private void Awake()
    {
        for (int i = 0; i < rangeUpgrades; i++)
        {
            contactObjectDistance += upgradeAmount;
        }
    }

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
                    ThrowHammer(magicObj.level);
                }
            }
        }
    }

    public void UpgradeHammerRange()
    {
        if (rangeUpgrades * upgradeAmount != contactObjectDistance)
        {
            contactObjectDistance = rangeUpgrades * upgradeAmount;
            destroyOnContactObject.transform.position = releasePosition.transform.position + new Vector3(0, 0, contactObjectDistance);
        }
    }

    public void ThrowHammer(int amountToCreate)
    {
        switch (amountToCreate)
        {
            case 1:
                Debug.Log("Single Shot");
                break;
            case 2:
                Debug.Log("Double Shot");
                break;
            case 3:
                
                break;
            default:
                break;
        }
        for (int i = 0; i < amountToCreate; i++)
        {
            GameObject hammerInstance = Instantiate(hammerPrefab, releasePosition.position +  new Vector3(-2 + i, 0, 0) , Quaternion.identity);
            Rigidbody rb = hammerInstance.GetComponent<Rigidbody>();
            rb.AddForce(Vector3.forward * 800f);
            hammerInstance.AddComponent<DisposableThrowable>();
        }
    }

}
