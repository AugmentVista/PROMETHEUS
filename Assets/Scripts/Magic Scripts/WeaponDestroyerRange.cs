using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDestroyerRange : MonoBehaviour
{
    Transform DestroyWeaponTransform;

    public int range;
    private int lastRange = 0;

    private void Start()
    {
        DestroyWeaponTransform = GetComponent<Transform>();
    }

    public void RangeUp()
    {
        range += 1;
    }

    public void ResetRange()
    {
        range = 0;
        lastRange = range;
    }

    void Update()
    {
        if (lastRange < range)
        {
            DestroyWeaponTransform.position += new Vector3(0, 0, 8);
            lastRange = range;
        }
    }
}
