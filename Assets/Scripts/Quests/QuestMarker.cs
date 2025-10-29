using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

public class QuestMarker : MonoBehaviour
{
    
    [SerializeField] private float timeToExpire = 60f;
    [SerializeField] private float timer;
    
    [SerializeField] private Button markerButton; // Botón que se clickea
    
    private MapSceneUIManager _uiManager; 

    private QuestSO quest;

    public TextMeshProUGUI timerText;
    private QuestSpawner spawner;
    private DayTimeManager _dayTimeManager;

    public void Setup(QuestSO QuestSO, QuestSpawner questSpawner, MapSceneUIManager uiManager)
    {
        quest = QuestSO;
        spawner = questSpawner;
        timer = timeToExpire;
        _uiManager = uiManager; // Guardamos la referencia al director
        _dayTimeManager = DayTimeManager.Instance;
        markerButton.onClick.RemoveAllListeners();
        markerButton.onClick.AddListener(OnMarkerClicked);
        UpdateTimerUI();
    }


    void Update()
    {
        if (_dayTimeManager.IsPaused()) return;

        timer -= Time.deltaTime;
        UpdateTimerUI();

        if (timer <= 0f)
        {
            ExpireQuest();
        }
    }

    void UpdateTimerUI()
    {
        timerText.text = $"{Mathf.Ceil(timer)}s";
    }

    void ExpireQuest()
    {
        spawner.RemoveMarker(gameObject);
    }

    void OnMarkerClicked()
    {
        _uiManager.OnQuestMarkerSelected(quest, this); // Le avisamos al director
    }

    public void OnQuestAccepted()
    {
        spawner.RemoveMarker(gameObject); // Ya no gestiona el sprite
    }
}