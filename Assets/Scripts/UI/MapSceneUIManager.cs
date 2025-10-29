using UnityEngine;

public class MapSceneUIManager : MonoBehaviour
{
    [Header("Paneles de UI")]
    [SerializeField] private QuestPopupUI questPopup;
    [SerializeField] private AvailableAdventurersUI availableAdventurersPanel;
    //[SerializeField] private GameObject activeQuestsPanel;

    private DayTimeManager _dayTimeManager;

    void Awake()
    {
        // Inicializamos el pop-up pasándole una referencia a nosotros mismos.
        questPopup.Initialize(this);
    }

    void Start()
    {
        _dayTimeManager = DayTimeManager.Instance;

        questPopup.gameObject.SetActive(false);
    }

    // --- MÉTODOS PÚBLICOS LLAMADOS POR OTROS SCRIPTS ---
    public void OnQuestMarkerSelected(QuestSO quest, QuestMarker marker)
    {
        // El director es quien pausa el juego y muestra el pop-up.
        _dayTimeManager.PauseDay(); // Pausa genérica
        questPopup.Show(quest, marker);
    }

    // Lo llama QuestPopupUI cuando se cierra (confirmando o cancelando).
    public void OnQuestPopupClosed()
    {
        // El director reanuda el juego.
        _dayTimeManager.ResumeDay();
    }
}