using UnityEngine;
using System.Collections.Generic; // Necesario para usar Listas

public class RosterPanelUI : MonoBehaviour
{
    [Header("Referencias")]
    public Transform cardContainer; // El contenedor con el Grid Layout Group
    public GameObject tavernRosterCardPrefab; // El prefab de la carta del aventurero
    public AdventurerDetailsUI adventurerDetailsPanel; // Referencia a nuestro nuevo panel

    private void OnEnable()
    {
        AdventurerManager.OnRosterChanged += PopulateRoster;
        PopulateRoster();
    }

    private void OnDisable()
    {
        AdventurerManager.OnRosterChanged -= PopulateRoster;
    }

    void Start()
    {
        // Al iniciar, poblamos la lista con los aventureros actuales.
        adventurerDetailsPanel.gameObject.SetActive(false);
        ApplyRest();
        PopulateRoster();
    }
    private void ApplyRest()
    { 
        List<AdventurerInstance> allAdventurers = AdventurerManager.Instance.GetAllAdventurers();

        foreach (AdventurerInstance adventurer in allAdventurers)
        {

            if (adventurer.IsResting)
            {
                adventurer.PerformRest(60);
            }
        }
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
            GameObject cardGO = Instantiate(tavernRosterCardPrefab, cardContainer);

            // Obtenemos la referencia al NUEVO script de la carta
            var cardUI = cardGO.GetComponent<TavernRosterCardUI>();
            if (cardUI != null)
            {
                cardUI.Setup(adventurer);
                cardUI.manageButton.onClick.AddListener(() => OnManageAdventurerClicked(adventurer, cardUI));
            }
        }
    }

    private void OnManageAdventurerClicked(AdventurerInstance adventurer, TavernRosterCardUI cardUI)
    {
        adventurerDetailsPanel.Show(adventurer, cardUI);
    }
}