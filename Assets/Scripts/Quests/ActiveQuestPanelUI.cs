using System.Collections;
using UnityEngine;

public class ActiveQuestPanelUI : MonoBehaviour
{
    [Header("UI")]
    private Transform activeQuestPanel;
    public GameObject questActiveCardPrefab;

    public void Awake()
    {
        activeQuestPanel = transform;
        if (activeQuestPanel == null)
        {
            Debug.LogError("No se encontró el panel de misiones activas en el objeto: " + gameObject.name);
        }
    }

    private void OnEnable()
    {
        if (QuestManager.Instance != null)
        {
            // Suscribimos nuestro método 'OnQuestLaunched' al evento del QuestManager.
            QuestManager.OnQuestLaunched += OnQuestLaunched;
            Debug.Log("Suscripción al evento OnQuestLaunched desde ActiveQuestPanel AÑADIDA.");
        }
    }

    // Se ejecuta cuando el objeto se desactiva. ¡CRUCIAL!
    private void OnDisable()
    {
        // Si el QuestManager todavía existe, nos desuscribimos.
        // Esto es crucial para evitar suscripciones duplicadas y fugas de memoria.
        if (QuestManager.Instance != null)
        {
            QuestManager.OnQuestLaunched -= OnQuestLaunched;
            Debug.Log("Suscripción al evento OnQuestLaunched QUITADA.");
        }
    }

    private void OnQuestLaunched(QuestInstance questInstance, Vector3 questPosition)
    {
        // Aquí podrías actualizar la UI si es necesario
        GameObject cardGO = Instantiate(questActiveCardPrefab, activeQuestPanel);
        var cardUI = cardGO.GetComponent<QuestActiveCardUI>();
        cardUI.Setup(questInstance);
        questInstance.AttachCard(cardUI);
    }



    public void OnDestroy()
    {
        if (QuestManager.Instance != null)
        {
            QuestManager.OnQuestLaunched -= OnQuestLaunched;
        }
    }
}