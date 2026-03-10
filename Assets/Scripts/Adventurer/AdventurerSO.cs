using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New Adventurer", menuName = "Game/Adventurer")]
public class AdventurerSO : ScriptableObject
{
    public Sprite portrait;
    public int baseStrength;
    public ClassType classType;
    public Sprite ClassTypeIcon;
    public List<PerkSO> innatePerks = new List<PerkSO>();
}
