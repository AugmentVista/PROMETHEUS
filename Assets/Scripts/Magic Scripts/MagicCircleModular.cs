using System;
using UnityEngine;

public class MagicCircleModular : MonoBehaviour
{
    public MagicCircle_ScriptableObject scriptableMagic;
    [SerializeField] Transform DestroyWeaponTransform;

    public int level;
    public int range;
    private int lastRange = 0;

    public string type;

    void Start()
    {
        level = scriptableMagic.level;
        type = scriptableMagic.ability.ToString();
    }

    public void RangeUp()
    {
        if (range < 5)
        { 
            range++;
        }
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
            DestroyWeaponTransform.position += new Vector3(0, 0, 10);
            lastRange = range;
        }
    }

    public bool IsTypeMatch(string typeToCheck)
    {
        return type.Equals(typeToCheck, StringComparison.OrdinalIgnoreCase);
    }
}
