using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class QuestSpawner : MonoBehaviour
{
    public QuestPoolSO questPool;
    public GameObject markerPrefab;
    public Transform guildOriginPoint;
    public Transform canvasMarkersWorld;
    public MapSceneUIManager mapSceneUIManager;
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
        if (activeMarkers.Count < maxQuestsOnMap)
        {
            SpawnQuestMarker();
        }
    }

    void SpawnQuestMarker()
    {
        if (activeMarkers.Count >= maxQuestsOnMap) return;

        HexTile targetTile = FindValidSpawnTile();
        if (targetTile == null)
        {
            Debug.LogWarning("QuestSpawner: No se encontró un tile válido para generar la misión.");
            return;
        }

        // Elegir una quest aleatoria del pool
        var quest = questPool.GetRandomQuest();

        Vector3 spawnPosition = MapGraph.Instance.GetWorldPositionFromTile(targetTile);

        var marker = Instantiate(markerPrefab, canvasMarkersWorld);
        marker.transform.position = spawnPosition; //+ new Vector3(0, 0.5f, 0); // Ajuste para que esté sobre el tile


        // Configurar el marker con la quest y el tile
        var markerScript = marker.GetComponent<QuestMarker>();
        markerScript.Setup(quest, this, mapSceneUIManager); // le pasa la quest y el spawner para callbacks

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
    
        private HexTile FindValidSpawnTile()
    {
        // Lógica para encontrar un buen lugar:
        // - Obtiene todos los tiles del grafo: MapGraph.Instance.graph.Values.ToList()
        // - Filtra los que no están ocupados por un edificio o por otra misión.
        // - Elige uno aleatorio de la lista de candidatos.
        
        var validTiles = MapGraph.Instance.graph.Values
            .Where(tile => !tile.HasQuest /* && !tile.HasQuest */)
            .ToList();

        if (validTiles.Count > 0)
        {
            return validTiles[Random.Range(0, validTiles.Count)];
        }

        return null; // No hay tiles válidos
    }

}
