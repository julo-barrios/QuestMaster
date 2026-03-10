using System.Collections.Generic;
using UnityEngine;

public class QuestOutcomeBuilder
{
    // Accumulators for resolution
    private int _partyStrength = 0;
    private int _requiredStrength = 0;
    private float _strengthMultiplier = 1f;
    private float _goldMultiplier = 1f;
    private float _xpMultiplier = 1f;
    private int _stressAdded = 0;
    private float _performancePenalty = 0f;
    
    private readonly List<string> _notes = new List<string>();

    public QuestOutcomeBuilder SetBaseRequirements(int minStrength)
    {
        _requiredStrength = minStrength;
        return this;
    }

    public QuestOutcomeBuilder AddEnemies(List<EnemyInstance> enemies)
    {
        if (enemies == null) return this;
        foreach (var enemy in enemies)
        {
            _requiredStrength += enemy.currentStrength;
            
            foreach (var perk in enemy.currentPerks)
            {
                if (perk.perkType == PerkType.Strong)
                {
                     _requiredStrength = Mathf.RoundToInt(_requiredStrength * 1.2f);
                     AddNote($"Enemigo con atributo Fuerte: Los requisitos de fuerza de la misión aumentaron significativamente.");
                }
            }
        }
        return this;
    }

    public QuestOutcomeBuilder AddParty(List<AdventurerInstance> party)
    {
        if (party == null) return this;
        foreach (var adv in party)
        {
            _partyStrength += adv.currentStrength;
            
            // Check Perks
            foreach (var perk in adv.CurrentPerks)
            {
                ApplyPerk(perk);
            }
        }
        return this;
    }

    public QuestOutcomeBuilder ApplySynergy(float multiplier)
    {
        if (multiplier != 1f)
        {
            _strengthMultiplier *= multiplier;
            AddNote($"Sinergia de grupo aplicada: Fuerza total magnificada (x{multiplier:F2}).");
        }
        return this;
    }

    public QuestOutcomeBuilder ApplyGuildBuffs()
    {
        // Placeholder for Guild / Patron buffs in the future
        // e.g. _goldMultiplier += GuildManager.CurrentPatron.GoldBonus;
        return this;
    }

    private void ApplyPerk(PerkSO perk)
    {
        if (perk.perkType == PerkType.Problematic)
        {
            _performancePenalty += 0.1f;
            _stressAdded += 5;
            AddNote($"Aventurero {perk.perkName} presente: Causó tensión extra y penalizó el rendimiento.");
        }
        else if (perk.perkType == PerkType.Greedy)
        {
            _goldMultiplier += 0.2f;
            _xpMultiplier += 0.2f;
            AddNote($"Aventurero {perk.perkName} presente: El grupo priorizó encontrar más oro y experiencia.");
        }
        // Additional perks easily slot in here
    }

    public QuestOutcomeBuilder AddNote(string note)
    {
        _notes.Add(note);
        return this;
    }

    public QuestOutcome ResolveAndBuild(QuestSO questData)
    {
        // 1. Calculate Performance Index
        int finalStrength = Mathf.RoundToInt(_partyStrength * _strengthMultiplier);
        float performanceIndex = (_requiredStrength > 0) ? (float)finalStrength / _requiredStrength : 1.5f;

        // Apply any penalties (e.g. from perks)
        performanceIndex -= _performancePenalty;
        Debug.Log($"[Builder] Fuerza Final: {finalStrength} | Fuerza Requerida: {_requiredStrength} | Índice de Rendimiento: {performanceIndex:P2}");

        // 2. Roll for Success
        float baseSuccessChance = Mathf.Clamp(performanceIndex * 0.7f, 0.1f, 0.9f); 
        float roll = UnityEngine.Random.value;
        bool isSuccess = roll <= baseSuccessChance;
        
        QuestResultData baseOutcome = null;

        if (isSuccess)
        {
            if (performanceIndex >= 1.5f && questData.successOutcomes.Count > 0)
                baseOutcome = questData.successOutcomes[0];
            else if (performanceIndex >= 1.0f && questData.successOutcomes.Count > 1)
                baseOutcome = questData.successOutcomes[1];
            else if (questData.successOutcomes.Count > 2)
                baseOutcome = questData.successOutcomes[2];
            else if (questData.successOutcomes.Count > 0)
                baseOutcome = questData.successOutcomes[0];
        }
        else
        {
            if (performanceIndex < 0.5f && questData.failureOutcomes.Count > 1)
                baseOutcome = questData.failureOutcomes[1]; 
            else if (questData.failureOutcomes.Count > 0)
                baseOutcome = questData.failureOutcomes[0];
        }

        // 3. Construct Final POCO
        string label = baseOutcome?.resultLabel ?? (isSuccess ? "Éxito" : "Fracaso");
        int finalGold = Mathf.RoundToInt((baseOutcome?.goldGain ?? 0) * _goldMultiplier);
        int finalXP = Mathf.RoundToInt((baseOutcome?.xpGain ?? 0) * _xpMultiplier);
        int finalFame = baseOutcome?.fameGain ?? 0;
        int finalStress = (baseOutcome?.stressChange ?? 0) + _stressAdded;

        if (baseOutcome != null && !string.IsNullOrEmpty(baseOutcome.resultDescription))
        {
            _notes.Insert(0, baseOutcome.resultDescription); // Place the narrative base note at the top
        }

        return new QuestOutcome(
            isSuccess,
            label,
            finalGold,
            finalXP,
            finalFame,
            finalStress,
            new List<string>(_notes)
        );
    }
}
