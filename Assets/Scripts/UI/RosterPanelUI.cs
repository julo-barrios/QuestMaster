using UnityEngine;
using System.Collections.Generic; // Necesario para usar Listas

public class RosterPanelUI : MonoBehaviour
{
    [Header("Referencias")]
    public Transform cardContainer; // El contenedor con el Grid Layout Group
    public GameObject tavernRosterCardPrefab; // El prefab de la carta del aventurero

    void Start()
    {
        // Al iniciar, poblamos la lista con los aventureros actuales.
        PopulateRoster();
    }

public void PopulateRoster()
    {
        foreach (Transform child in cardContainer)
        {
            Destroy(child.gameObject);
        }

        List<AdventurerInstance> allAdventurers = AdventurerManager.Instance.GetAllAdventurers();

        foreach (AdventurerInstance adventurer in allAdventurers)
        {

            if (adventurer.IsResting)
            {
                adventurer.PerformRest(60);
            }

            GameObject cardGO = Instantiate(tavernRosterCardPrefab, cardContainer);

            // Obtenemos la referencia al NUEVO script de la carta
            var cardUI = cardGO.GetComponent<TavernRosterCardUI>();
            if (cardUI != null)
            {
                cardUI.Setup(adventurer);

                // Aquí conectaremos la lógica del botón "Gestionar" en el futuro.
                // Por ahora, lo dejamos listo.
                cardUI.manageButton.onClick.AddListener(() => OnManageAdventurerClicked(cardUI));
            }
        }
    }

    private void OnManageAdventurerClicked(TavernRosterCardUI adventurer)
    {
        // Invierte el estado actual de descanso.
        adventurer.RestAdventurer();
        adventurer.UpdateStatusText();
    }
}