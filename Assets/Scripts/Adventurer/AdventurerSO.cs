using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Adventurer", menuName = "Game/Adventurer")]
public class AdventurerSO : ScriptableObject
{
    public Sprite portrait;
    public int baseStrength;
    public ClassType classType;
    public Sprite ClassTypeIcon;
}
