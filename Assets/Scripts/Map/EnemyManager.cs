using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public List<Faction> EnemyFactions { get; } = new();

    public void UpdateEnemyBehavior()
    {
        foreach (var faction in EnemyFactions)
            Debug.Log("Expanding faction: " + faction);
            //faction.Expand();
    }
}
