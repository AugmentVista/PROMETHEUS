using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBridge : MonoBehaviour
{
    public GameObject bridgePrefab;
    private float bridgeZ = -82.5f;
    private GameObject parentOfBridge;

    private void Awake()
    {
        bridgePrefab = GameObject.Find("Test_Prefab_BridgeSection");
    }
    public void CreateBridge()
    {
        parentOfBridge = GameObject.Find("Extention");

        Transform ExtentionTransform = parentOfBridge.transform;

        Instantiate(bridgePrefab, new Vector3(0,0, bridgeZ), Quaternion.identity);
    }
}
