using System;
using UnityEngine;

public class GuildBuilding : MonoBehaviour
{
    public string buildingName = "Gremio de Aventureros";
    public GameObject travelSpritePrefab;
    public Transform guildOriginPoint;
    public int level = 1;

    private void OnEnable()
    {
        if (QuestManager.Instance != null)
        {
            // Suscribimos nuestro método 'OnQuestLaunched' al evento del QuestManager.
            QuestManager.OnQuestLaunched += OnQuestLaunched;
            Debug.Log("Suscripción al evento OnQuestLaunched AÑADIDA.");
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

    private void OnMouseDown()
    {
        Debug.Log($"Has hecho clic en {buildingName}, Nivel {level}.");
        // Aquí podrías abrir el menú de gestión del gremio.
        // UIManager.Instance.OpenGuildMenu();
    }

    public void OnQuestLaunched(QuestInstance quest, Vector3 questPosition)
    {
        Debug.Log($"El edificio {buildingName} ha lanzado la misión: {quest.questData.questTitle}");
        // Aquí podrías actualizar la UI del edificio o hacer otras acciones.
        // Instanciar sprite visual
        GameObject spriteGO = Instantiate(travelSpritePrefab, guildOriginPoint.position, Quaternion.identity);
        var spriteInstance = spriteGO.GetComponent<AdventurerTravelSprite>();
        spriteInstance.Setup(
            guildOriginPoint.position,
            questPosition);

        spriteInstance.HeadToQuest(() => quest.OnArrivedAtQuestLocation());

        quest.SetTravelerSprite(spriteInstance);
    }
}