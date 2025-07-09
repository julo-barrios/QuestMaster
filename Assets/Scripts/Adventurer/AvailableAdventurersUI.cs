
using System.Collections.Generic;
using UnityEngine;

public class AvailableAdventurersUI : MonoBehaviour 
{

    public static AvailableAdventurersUI Instance;

    public Transform availableAdventurersContainer;
    [SerializeField]private GameObject adventurerCardPrefab;
    private Transform adventurerCardsContainer;

    void Awake()
    {
        Instance = this;
    }


    void Start()
    {
        adventurerCardsContainer = gameObject.transform;
        LoadAvailableAdventurers();
    }
    void LoadAvailableAdventurers()
    {
        // Suplantar con tu lista real
        List<AdventurerInstance> available = AdventurerManager.Instance.GetAvailableAdventurers();

        foreach (Transform child in adventurerCardsContainer)
            Destroy(child.gameObject);

        foreach (var adventurer in available)
        {
            AddAvailableAdventurer(adventurer);
        }
    }

    public void AddAvailableAdventurer(AdventurerInstance adv)
    {
        var cardGO = Instantiate(adventurerCardPrefab, adventurerCardsContainer);
        var cardUI = cardGO.GetComponent<AdventurerCardUI>();
        cardUI.Setup(adv);

        cardUI.GetComponent<CanvasGroup>().blocksRaycasts = true;
        cardUI.droppedInSlot = false;
    }

    public void ReturnToAvailable(AdventurerInstance adv)
    {
        var cardGO = Instantiate(adventurerCardPrefab);
        var cardUI = cardGO.GetComponent<AdventurerCardUI>();
        cardUI.Setup(adv);
        //cardUI.GetComponent<Button>().onClick.AddListener(() => AssignAdventurer(adv, cardGO));
    }
    
}