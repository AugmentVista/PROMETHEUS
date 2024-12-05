using UnityEngine;

[CreateAssetMenu(fileName = "Magic Circle", menuName = "Magics", order = 1)]
public class MagicCircle_ScriptableObject : ScriptableObject
{
    public int level;

    public Ability ability;
    public enum Ability
    {
        Duplicate,
        Fortify,
        Vitality,
        Polymorph
    }
}