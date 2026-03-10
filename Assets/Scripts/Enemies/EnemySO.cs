using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Game/Enemy")]
public class EnemySO : ScriptableObject
{
    public string enemyName;
    public int baseStrength; // Adds to the quest's required strength
    public List<PerkSO> innatePerks = new List<PerkSO>();
}
