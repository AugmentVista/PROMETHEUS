using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBridge : MonoBehaviour
{
    public GameObject bridgePrefab;


    public void CreateBridge()
    {
        Instantiate(bridgePrefab, new Vector3(0,3,0), Quaternion.identity);
    }


}
