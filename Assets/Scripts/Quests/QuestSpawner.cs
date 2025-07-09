using UnityEngine;
using System.Collections.Generic;

public class QuestSpawner : MonoBehaviour
{
    public QuestPoolSO questPool;
    public GameObject markerPrefab;
    public Transform guildOriginPoint;
    public Transform canvasMarkersWorld;

    public List<Transform> possibleMarkerPositions;
    public int maxQuestsOnMap = 5;

    private List<GameObject> activeMarkers = new();

    void Start()
    {
        GenerateInitialQuests();
    }

    public void GenerateInitialQuests()
    {
        for (int i = 0; i < maxQuestsOnMap; i++)
        {
            SpawnQuestMarker();
        }
    }

    void Update()
    {
        if(activeMarkers.Count < maxQuestsOnMap)
        {
            SpawnQuestMarker();
        }
    }

    void SpawnQuestMarker()
    {
        if (activeMarkers.Count >= maxQuestsOnMap) return;

        var quest = questPool.GetRandomQuest();
        var marker = Instantiate(markerPrefab, canvasMarkersWorld);
        var randomPos = possibleMarkerPositions[Random.Range(0, possibleMarkerPositions.Count)];
        marker.transform.localPosition = randomPos?.position * 100 ?? Vector3.zero;

        var markerScript = marker.GetComponent<QuestMarker>();
        markerScript.Setup(quest, this); // le pasa la quest y el spawner para callbacks

        markerScript.guildOriginPoint = guildOriginPoint;
        activeMarkers.Add(marker);
    }

    public void RemoveMarker(GameObject marker)
    {
        if (QuestPopupUI.Instance.GetCurrentMarker() == marker)
        {
            QuestPopupUI.Instance.Hide(); // Forzar cierre si sigue abierto
        }
        activeMarkers.Remove(marker);
        Destroy(marker);
        // Opcional: podrías generar uno nuevo o esperar al siguiente día
    }
}
