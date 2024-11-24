using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerThrow : MonoBehaviour
{
    [SerializeField] GameObject destroyOnContactObject;
    [SerializeField] GameObject hammerPrefab;

    [SerializeField] Rigidbody[] rb = new Rigidbody[2];

    [SerializeField] Transform releasePosition;

    [SerializeField] float speed = 10f;

    [SerializeField] Vector3 offset = new Vector3(1, 0, 0);

    private int rangeUpgrades = 1;
    private int upgradeAmount = 10;
    private int contactObjectDistance = 1;

    private void Awake()
    {
        for (int i = 0; i < rangeUpgrades; i++)
        {
            contactObjectDistance += upgradeAmount;
        }
        destroyOnContactObject.transform.position = releasePosition.transform.position + new Vector3 (0, 0, contactObjectDistance);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag);
        if (other.CompareTag("Release"))
        {
            Debug.Log(other.name);
            ThrowHammer();
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

    public void ThrowHammer()
    {
        Debug.Log("Throwing Hammers");
        GameObject firstHammerInstance = Instantiate(hammerPrefab, releasePosition.position + offset, Quaternion.identity);

        GameObject secondHammerInstance = Instantiate(hammerPrefab, releasePosition.position + -offset, Quaternion.identity);

        
        rb[0] = firstHammerInstance.GetComponent<Rigidbody>();
        rb[1] = secondHammerInstance.GetComponent<Rigidbody>();

        Debug.Log(rb[0], rb[1]);

        foreach (Rigidbody rigidbody in rb)
        {
            rigidbody.velocity = Vector3.forward * speed;
            //rigidbody.gameObject.transform.SetParent(null);
        }

        firstHammerInstance.AddComponent<DisposableThrowable>();
        secondHammerInstance.AddComponent<DisposableThrowable>();
    }

}
