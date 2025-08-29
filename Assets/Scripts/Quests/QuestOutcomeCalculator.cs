using UnityEngine;
using System.Collections.Generic;

public static class QuestOutcomeCalculator
{
    public static QuestResultData Calculate(QuestInstance quest)
    {
        // 1. Calculamos la fuerza total del equipo
        int totalPartyStrength = 0;
        foreach (var adventurer in quest.Party)
        {
            totalPartyStrength += adventurer.currentStrength;
        }

        int requiredStrength = quest.questData.requirements.minimumTotalStrength;

        // 2. Calculamos el "Índice de Rendimiento"
        // Si el requisito es 0, evitamos división por cero.
        float performanceIndex = (requiredStrength > 0) ? (float)totalPartyStrength / requiredStrength : 1.5f;
        Debug.Log($"Índice de Rendimiento: {performanceIndex:P2}"); // Muestra como porcentaje

        // 3. Determinamos si es un éxito o un fracaso general
        // Una tirada de dados sigue siendo necesaria para evitar que sea 100% predecible.
        // Pero el rendimiento afecta enormemente la probabilidad.
        float baseSuccessChance = Mathf.Clamp(performanceIndex * 0.7f, 0.1f, 0.9f); // 70% del índice, con un mínimo de 10% y un máximo de 90%
        float roll = Random.value;

        if (roll <= baseSuccessChance)
        {
            // --- ÉXITO ---
            // Si el rendimiento es muy alto (>150%), es un éxito total (Elemento 0).
            if (performanceIndex >= 1.5f && quest.questData.successOutcomes.Count > 0)
                return quest.questData.successOutcomes[0];
            
            // Si el rendimiento es normal (100-149%), es un éxito estándar (Elemento 1).
            if (performanceIndex >= 1.0f && quest.questData.successOutcomes.Count > 1)
                return quest.questData.successOutcomes[1];

            // Si el rendimiento es bajo (menos de 100%), es un éxito costoso (Elemento 2).
            if (quest.questData.successOutcomes.Count > 2)
                return quest.questData.successOutcomes[2];
            
            // Fallback al mejor éxito posible si no hay suficientes definidos.
            return quest.questData.successOutcomes[0];
        }
        else
        {
            // --- FRACASO ---
            // La lógica se invierte. Un rendimiento muy bajo lleva al peor fracaso.
            if (performanceIndex < 0.5f && quest.questData.failureOutcomes.Count > 1)
                return quest.questData.failureOutcomes[1]; // El peor fracaso

            // Un fracaso estándar
            if (quest.questData.failureOutcomes.Count > 0)
                return quest.questData.failureOutcomes[0];

            // Fallback si no hay fracasos definidos (no debería pasar)
            return null;
        }
    }
}