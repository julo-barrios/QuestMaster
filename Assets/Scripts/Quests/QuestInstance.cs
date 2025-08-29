using System.Collections.Generic;
using UnityEngine;

public class QuestInstance
{
    public List<AdventurerInstance> Party;
    public QuestState currentState = QuestState.TravelingToQuest;
    public float totalDays;
    public float daysRemaining;
    public QuestSO questData;
    public float timeRemaining;
    public bool isCompleted = false;
    public AdventurerTravelSprite travelersSprite;
    public QuestCompletionOutcome CompletionOutcome;
    public QuestResultData Result { get; private set; } // El resultado final se guarda aquí

    public QuestActiveCardUI cardUI;
    
    public QuestInstance(QuestSO questquestData, List<AdventurerInstance> party)
    {
        questData = questquestData;
        Party = party;
        totalDays = questquestData.DurationInDays();
        daysRemaining = totalDays;
        timeRemaining = daysRemaining * 300;
    }
        
    public void SetTravelerSprite(AdventurerTravelSprite sprite)
    {
        travelersSprite = sprite;
    }
    public void AttachCard(QuestActiveCardUI ui)
    {
        cardUI = ui;
    }

    public void OnArrivedAtQuestLocation()
    {
        Debug.Log("📍 Llegamos al lugar de la quest.");

        currentState = QuestState.ExecutingQuest;
        timeRemaining = questData.DurationInDays() * DayTimeManager.Instance.dayDurationSeconds;
        Debug.Log($"La party llegó a la quest '{questData.questTitle}'. Ejecutando...");
    }


    public void UpdateQuest(float deltaTime)
    {
        if (isCompleted || DayTimeManager.Instance.IsGamePausedByPopup) return;

        switch (currentState)
        {
            case QuestState.TravelingToQuest:
                HandlePreEvent(); // opcional
                break;

            case QuestState.ExecutingQuest:
                HandleOnQuestEvent(); // opcional
                cardUI?.UpdateProgress(1f - (timeRemaining / (questData.DurationInDays() * DayTimeManager.Instance.dayDurationSeconds)));

                timeRemaining -= deltaTime;

                if (timeRemaining <= 0f)
                {
                    AdvanceState();
                }
                break;
        }

    }

    private void AdvanceState()
    {
        HandleQuestOutcome(); // calcula heridos, muertos, oro, etc
        currentState = QuestState.ReturningToGuild;
        travelersSprite.Setup(QuestManager.Instance.guildOriginPoint.position, () => OnReturnedToGuild());
        Debug.Log($"Quest '{questData.questTitle}' completada. Volviendo...");
    }

    private void HandlePreEvent()
    {
        // // 20% de probabilidad de evento
        // if (Random.value < 0.2f)
        // {
        //     Debug.Log("📌 Evento en el camino: emboscada de bandidos.");
        //     // Acá podrías generar daños leves, decisión, etc.
        // }
    }

    private void HandleOnQuestEvent()
    {
        // // 20% de probabilidad de evento
        // if (Random.value < 0.2f)
        // {
        //     Debug.Log("📌 Evento Durante la quest: emboscada de bandidos.");
        //     // Acá podrías generar daños leves, decisión, etc.
        // }
    }

    private void HandleQuestOutcome()
    {
        // ANTES: CompletionOutcome = GetRandomOutcome();
        // AHORA:
        Result = QuestOutcomeCalculator.Calculate(this);
    }

    private void OnReturnedToGuild()
    {
        Debug.Log("🏠 La party regresó al gremio.");
        currentState = QuestState.Completed;
        QuestManager.Instance.OnQuestCompleted(this);
    }

    public QuestCompletionOutcome GetRandomOutcome()
    {
        float roll = Random.value;
        float cumulative = 0f;

        foreach (var outcome in questData.completionOutcomes)
        {
            cumulative += outcome.probability;
            if (roll <= cumulative)
            {
                return outcome;
            }
        }

        // Fallback en caso de que no caiga en ninguna
        return questData.completionOutcomes[0];
    }
}
