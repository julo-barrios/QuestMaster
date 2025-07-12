using System.Collections.Generic;
using UnityEngine;

public class RecruitmentPanelUI : MonoBehaviour
{
    [Header("Referencias")]
    public Transform cardContainer;
    public GameObject recruitCardPrefab;

    private List<AdventurerSO> _adventurerTemplates;

    void Start()
    {
        // Cargamos los templates de aventureros que ya tienes
        _adventurerTemplates = new List<AdventurerSO>(Resources.LoadAll<AdventurerSO>("AdventurerTemplates"));
    }

    // Este método se llama cuando se abre la pestaña
    private void OnEnable()
    {
        _adventurerTemplates = new List<AdventurerSO>(Resources.LoadAll<AdventurerSO>("AdventurerTemplates"));
        GenerateNewRecruits();
    }

    public void GenerateNewRecruits()
    {
        // Limpiamos los reclutas anteriores
        foreach (Transform child in cardContainer)
        {
            Destroy(child.gameObject);
        }

        // Generamos, por ejemplo, 3 nuevos reclutas
        for (int i = 0; i < 3; i++)
        {
            // 1. Crear un nuevo aventurero
            AdventurerSO template = _adventurerTemplates[Random.Range(0, _adventurerTemplates.Count)];
            AdventurerInstance newRecruit = new AdventurerInstance(template, 100, 100); // Stats base
            newRecruit.Rank = (QuestRank)Random.Range(1, 4); // Rango aleatorio entre E, D, C
            newRecruit.Name = "Recluta " + i; // Nombre temporal

            // 2. Calcular su costo
            int hiringCost = 100 + ((int)newRecruit.Rank * 50); // Lógica de costo simple

            // 3. Crear y configurar la tarjeta de UI
            GameObject cardGO = Instantiate(recruitCardPrefab, cardContainer);
            var cardUI = cardGO.GetComponent<RecruitCardUI>();
            cardUI.Setup(newRecruit, hiringCost);

            // 4. Asignar la lógica al botón "Contratar"
            cardUI.hireButton.onClick.AddListener(() => HireAdventurer(newRecruit, hiringCost, cardGO));
        }
    }

    private void HireAdventurer(AdventurerInstance adventurer, int cost, GameObject cardGO)
    {
        // Verificamos si tenemos suficiente oro
        if (GuildManager.Instance.SpendGold(cost))
        {
            // Si tenemos, lo añadimos al gremio
            AdventurerManager.Instance.AddAdventurerToRoster(adventurer);
            Debug.Log($"¡{adventurer.Name} se ha unido al gremio!");

            // Eliminamos la tarjeta del recluta contratado
            Destroy(cardGO);

            // Opcional: podrías añadir aquí una llamada para refrescar el panel de la plantilla
            // y ver al nuevo miembro inmediatamente.
        }
        else
        {
            // Si no hay suficiente oro, mostramos un mensaje
            Debug.Log("¡Oro insuficiente para contratar a este aventurero!");
        }
    }
    

    public void Reroll()
    {
        // Lógica para refrescar los reclutas
        if (GuildManager.Instance.SpendGold(50)) // Costo de reroll
        {
            GenerateNewRecruits();
            Debug.Log("Reclutas actualizados.");
        }
        else
        {
            Debug.Log("¡Oro insuficiente para actualizar los reclutas!");
        }
    }

}