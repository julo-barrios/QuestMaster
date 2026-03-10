using UnityEngine;
using System.Collections.Generic;

public static class QuestOutcomeCalculator
{
    public static QuestOutcome Calculate(QuestInstance quest)
    {
        // Iniciamos el Builder
        var builder = new QuestOutcomeBuilder();

        // 1. Configuramos las condiciones iniciales de la Quest
        builder.SetBaseRequirements(quest.questData.requirements.minimumTotalStrength)
               .AddEnemies(quest.Enemies);

        // 2. Evaluamos a los Aventureros (Fuerza base y Perks)
        builder.AddParty(quest.Party);

        // 3. Evaluamos Sinergias del Equipo
        float synergyMultiplier = SynergyManager.GetPartyStrengthMultiplier(quest.Party);
        builder.ApplySynergy(synergyMultiplier);

        // 4. Espacio para Buffs de Edificios / Patrones
        builder.ApplyGuildBuffs();

        // 5. El Builder procesa todos los cálculos matemáticos resultantes, 
        // tira los dados, y retorna el objeto POCO final con toda la info.
        return builder.ResolveAndBuild(quest.questData);
    }
}