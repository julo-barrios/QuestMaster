using UnityEngine;

[CreateAssetMenu(fileName = "New Perk", menuName = "Game/Perk")]
public class PerkSO : ScriptableObject
{
    public string perkName;
    [TextArea]
    public string description;
    public PerkType perkType;
    public float modifierValue; // A generic value that can be a percentage or flat amount based on PerkType
}
