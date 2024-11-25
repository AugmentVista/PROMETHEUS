using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicCircleModular : MonoBehaviour
{
    public MagicCircle_ScriptableObject scriptableMagic;
    [SerializeField] Transform DestroyWeaponTransform;

    public int level;
    private int lastLevel = 0;

    public string type;

    void Start()
    {
        level = scriptableMagic.level;
        type = scriptableMagic.ability.ToString();
    }

    void Update()
    {
        if (lastLevel < level)
        {
            DestroyWeaponTransform.position += new Vector3(0, 0, 10);
            lastLevel = level;
        }
    }

    public bool IsTypeMatch(string typeToCheck)
    {
        return type.Equals(typeToCheck, StringComparison.OrdinalIgnoreCase);
    }
}
