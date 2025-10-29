using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

public class QuestPopupUI : MonoBehaviour
{

    
    // --- Singleton ---
    public static QuestPopupUI Instance { get; private set; }
    
    [Header("UI References")]
    public TextMeshProUGUI questTitleText;
    public TextMeshProUGUI questDescriptionText;
    public TextMeshProUGUI rankText;
    public TextMeshProUGUI durationText;
    public TextMeshProUGUI requirementsText;
    public AsignedAdventurerContainerUI PartySlotContainer;
    private QuestSO currentQuest;
    private QuestMarker currentMarker;
    private MapSceneUIManager _uiManager; // Referencia al director

 // El director llamará a este método para inicializar.
    public void Initialize(MapSceneUIManager uiManager)
    {
        _uiManager = uiManager;
    }
    void Awake()
    {
        // --- Singleton Pattern ---
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }

        // Opcional: lo desactivás al iniciar
        gameObject.SetActive(false);
    }

    public void Show(QuestSO quest, QuestMarker marker)
    {
        currentQuest = quest;
        currentMarker = marker;

        questTitleText.text = quest.questTitle;
        questDescriptionText.text = quest.description;
        rankText.text = $"Rango: {quest.rank}";
        durationText.text = $"Duración: {quest.DurationInDays()} días";

        ClearPartySlots();

        gameObject.SetActive(true);
    }

    void ClearPartySlots()
    {
        PartySlotContainer.CreateNewSlots(currentQuest);
    }


    public QuestMarker GetCurrentMarker()
    {
        return currentMarker;
    }

    public void Hide()
    {
        gameObject.SetActive(false);
        currentMarker = null; // Limpia referencias
    }

    public void ConfirmQuest()
    {
        if (!PartySlotContainer.QuestRequirementsMet())
        {
            Debug.Log("Seleccioná al menos un aventurero!");
            return;
        }

        currentMarker.GetComponent<QuestMarker>().OnQuestAccepted();
        QuestManager.Instance.LaunchQuest(currentQuest, currentMarker.transform.position, PartySlotContainer.GetAdventurers());

        _uiManager.OnQuestPopupClosed(); // Avisa al director para que reanude el juego.

        Hide();
    }

    public void CloseQuestPopup()
    {
        PartySlotContainer.ReturnToAvailable();
        _uiManager.OnQuestPopupClosed();
        gameObject.SetActive(false);
    }

}
