using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance;
    public GameObject travelSpritePrefab;
    public Transform guildOriginPoint;

    private List<QuestInstance> activeQuests = new();

    [Header("UI")]
    public Transform activeQuestPanel;
    public GameObject questActiveCardPrefab;
    private DayTimeManager _dayTimeManager;
    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        
        _dayTimeManager = DayTimeManager.Instance;
    }

    public QuestInstance LaunchQuest(QuestSO questData, Vector3 questPosition, List<AdventurerInstance> party)
    {
        var questInstance = new QuestInstance(questData, party);
        activeQuests.Add(questInstance);


        GameObject cardGO = Instantiate(questActiveCardPrefab, activeQuestPanel);
        var cardUI = cardGO.GetComponent<QuestActiveCardUI>();
        cardUI.Setup(questInstance);
        questInstance.AttachCard(cardUI);
        
        // Instanciar sprite visual
        GameObject spriteGO = Instantiate(travelSpritePrefab, guildOriginPoint.position, Quaternion.identity);
        var spriteInstance = spriteGO.GetComponent<AdventurerTravelSprite>();
        spriteInstance.Setup(
            questPosition,
            () => questInstance.OnArrivedAtQuestLocation());
        questInstance.SetTravelerSprite(spriteInstance);
        return questInstance;
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
        Debug.Log($"Quest '{quest.questData.questTitle}' resultada.");

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

    foreach(var adventurer in quest.Party)
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
        Debug.Log($"Aventurero {adventurer.Name} volvió. Energía restante: {adventurer.CurrentEnergy}");
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
}
