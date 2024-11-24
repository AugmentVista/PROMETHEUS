using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerThrow : MonoBehaviour
{
    [SerializeField] GameObject destroyOnContactObject;
    [SerializeField] GameObject hammerPrefab;

    [SerializeField] Transform releasePosition;

    [SerializeField] float speed = 1000f;

    [SerializeField] Vector3 offset = new Vector3(0, 0, 0);

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
        GameObject firstHammerInstance = Instantiate(hammerPrefab, releasePosition.position, Quaternion.identity);

        // GameObject secondHammerInstance = Instantiate(hammerPrefab, releasePosition.position + -offset, Quaternion.identity);


        Rigidbody rb = firstHammerInstance.GetComponent<Rigidbody>();
        rb.AddForce(Vector3.forward * speed);
       // rb[1] = secondHammerInstance.GetComponent<Rigidbody>();

        
        firstHammerInstance.AddComponent<DisposableThrowable>();
        // secondHammerInstance.AddComponent<DisposableThrowable>();
        

       
    }

}
