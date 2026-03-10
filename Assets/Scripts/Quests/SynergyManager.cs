using System.Collections.Generic;
using UnityEngine;

public static class SynergyManager
{
    // Returns a multiplier for the party's total strength based on synergies
    public static float GetPartyStrengthMultiplier(List<AdventurerInstance> party)
    {
        if (party == null || party.Count == 0) return 1f;

        Dictionary<ClassType, int> classCounts = new Dictionary<ClassType, int>();
        foreach (var adv in party)
        {
            if (!classCounts.ContainsKey(adv.ClassType))
            {
                classCounts[adv.ClassType] = 0;
            }
            classCounts[adv.ClassType]++;
        }

        float multiplier = 1f;

        // Example Synergy: 3 of the same class give a 15% boost, 4 give 25%
        foreach (var kvp in classCounts)
        {
            if (kvp.Value == 3)
            {
                multiplier += 0.15f;
                Debug.Log($"Synergy activated! 3 {kvp.Key}s (+15% strength)");
            }
            else if (kvp.Value >= 4)
            {
                multiplier += 0.25f;
                Debug.Log($"Synergy activated! 4+ {kvp.Key}s (+25% strength)");
            }
        }

        // Example Synergy: All unique classes
        if (classCounts.Count >= 4)
        {
            // E.g., at least 4 different classes
            multiplier += 0.20f;
            Debug.Log($"Synergy activated! Diverse party (+20% strength)");
        }

        return multiplier;
    }
}
