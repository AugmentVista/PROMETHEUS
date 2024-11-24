using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicCircleModular : MonoBehaviour
{
    [SerializeField] MagicCircle_ScriptableObject scriptableMagic;

    public int Level;

    public string Type;


    void Start()
    {
        Level = scriptableMagic.level;

        Type = scriptableMagic.ability.ToString();
        Debug.LogError(Type);
    }

}
