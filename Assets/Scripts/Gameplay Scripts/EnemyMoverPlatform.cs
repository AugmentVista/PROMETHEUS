using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoverPlatform : MonoBehaviour
{
    public GameObject platform;
    Transform platformTransform;

    private void Start()
    {
        platformTransform = transform;
    }
    public void PushForward()
    {
        platformTransform.Translate(Vector3.forward * Time.deltaTime, Space.World);
    }
}
