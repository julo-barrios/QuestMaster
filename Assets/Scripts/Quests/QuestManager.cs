using System;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    private static QuestManager _instance;
    private static bool applicationIsQuitting = false;

    private List<QuestInstance> activeQuests = new();
    public static event Action<QuestInstance, Vector3> OnQuestLaunched;
    private DayTimeManager _dayTimeManager;

    public static QuestManager Instance
    {
        get
        {
            if (applicationIsQuitting)
            {
                return null;
            }
            // Si la instancia aún no existe...
            if (_instance == null)
            {
                // ...buscamos el prefab en la carpeta "Resources"...
                var prefab = Resources.Load<GameObject>("GameManagers");
                // ...y lo creamos en la escena.
                Instantiate(prefab);
            }
            return _instance;
        }
    }
    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }
        _instance = this;
        applicationIsQuitting = false;
        _dayTimeManager = DayTimeManager.Instance;
    }

    public void LaunchQuest(QuestSO questData, Vector3 questPosition, List<AdventurerInstance> party)
    {
        Debug.Log($"Lanzando quest: {questData.questTitle} con {party.Count} aventureros.");
        var questInstance = new QuestInstance(questData, party);
        activeQuests.Add(questInstance);

        OnQuestLaunched?.Invoke(questInstance, questPosition);
    }

    private void Update()
    {
        if (!_dayTimeManager) return;
        if (_dayTimeManager.IsPaused()) return;

        foreach (var quest in activeQuests)
        {
            quest.UpdateQuest(Time.deltaTime);
        }
    }



    public void OnQuestCompleted(QuestInstance quest)
    {
        /*QuestCompletionOutcome outcome = quest.GetRandomOutcome();

        Debug.Log($"Quest '{quest.questData.questTitle}' completada con resultado: {outcome.label}");

        // Aplicar efectos a aventureros
        foreach (var adv in quest.assignedParty)
        {
            adv.IsAvailable = true;

            // Simular efectos
            if (outcome.outcome.killedCount > 0)
            {
                adv.IsDead = true;
                outcome.outcome.killedCount--;
                continue;
            }

            // if (outcome.outcome.injuredCount > 0)
            // {
            //     adv.IsInjured = true;
            //     outcome.outcome.injuredCount--;
            //     continue;
            // }

            adv.GainXP(outcome.outcome.xpGain);
        }
        */
        // Aplicar fama, oro al guild
        GuildManager.Instance.AddFame(quest.Result.fameGain);
        GuildManager.Instance.AddGold(quest.Result.goldGain);

        foreach (var adventurer in quest.Party)
        {
            // --- LÓGICA A AÑADIR/MODIFICAR ---
            adventurer.GainXP(quest.Result.xpGain);
            adventurer.GainXP(quest.Result.xpGain);
            // Calculamos un costo de energía. Por ejemplo, 30 puntos por misión.
            int energyCost = 30;
            adventurer.CurrentEnergy -= energyCost;
            if (adventurer.CurrentEnergy < 0)
            {
                adventurer.CurrentEnergy = 0;
            }
            // ---------------------------------

            // Tu código existente para devolver al aventurero a la lista de disponibles.
            AvailableAdventurersUI.Instance.AddAvailableAdventurer(adventurer);
        }

        Destroy(quest.cardUI.gameObject);
    }

    public List<QuestInstance> GetActiveQuests()
    {
        return activeQuests;
    }
    
    public void OnDestroy()
    {
        // Si este es el Singleton original, activamos la bandera.
        if (_instance == this)
        {
            applicationIsQuitting = true;
        }
    }
}
