using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicCircleModular : MonoBehaviour
{
    [SerializeField] MagicCircle_ScriptableObject scriptableMagic;

    public int level;

    public string type;

    void Start()
    {
        level = scriptableMagic.level;

        type = scriptableMagic.ability.ToString();
    }

    public bool IsTypeMatch(string typeToCheck)
    {
        return type.Equals(typeToCheck, StringComparison.OrdinalIgnoreCase);
    }
}
