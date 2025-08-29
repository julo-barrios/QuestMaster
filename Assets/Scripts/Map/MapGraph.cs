using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

public class MapGraph : MonoBehaviour
{
    public static MapGraph Instance;

    [SerializeField] private Tilemap biomesTilemap;

    [SerializeField] private Grid grid; // Esta línea ya la tenías.
    // Mapea la posición de una celda a sus datos
    public Dictionary<Vector3Int, HexTile> graph;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        GenerateGraphFromTilemap();
    }

    private void GenerateGraphFromTilemap()
    {
        graph = new Dictionary<Vector3Int, HexTile>();

        // Recorremos cada tile pintado en el tilemap de biomas
        foreach (var pos in biomesTilemap.cellBounds.allPositionsWithin)
        {
            if (biomesTilemap.HasTile(pos))
            {
                TileBase tileBase = biomesTilemap.GetTile(pos);
                if (tileBase is BiomeTile biomeTile)
                {
                    // ¡Éxito! Ahora podemos leer sus datos directamente.

                    // 3. Creamos el nodo del grafo usando los datos del BiomeTile.
                    HexTile newHexNode = new HexTile(pos, biomeTile.biome);

                    // 4. Copiamos la lista de enemigos desde el tile al nodo del grafo.
                    newHexNode.PossibleEnemies = new List<string>(biomeTile.possibleEnemies);

                    // 5. Añadimos el nodo al grafo.
                    graph.Add(pos, newHexNode);
                }
            }
        }
        Debug.Log($"Grafo generado con {graph.Count} nodos a partir del mapa pintado a mano.");
    }

    // Función de ejemplo para usar el grafo
    public List<HexTile> GetNeighbors(Vector3Int position)
    {
        List<HexTile> neighbors = new List<HexTile>();
        // Lógica para encontrar vecinos en un grid hexagonal...
        // ... por ejemplo, con coordenadas axiales o cúbicas.
        return neighbors;
    }

    public HexTile GetTileFromWorldPosition(Vector3 worldPosition)
    {
        Vector3Int cellPosition = grid.WorldToCell(worldPosition);
        if (graph.ContainsKey(cellPosition))
        {
            return graph[cellPosition];
        }
        return null;
    }

    // --- MÉTODO NUEVO QUE FALTA ---
    // Recibe un nodo del grafo (HexTile) y devuelve su posición central en el mundo.
    public Vector3 GetWorldPositionFromTile(HexTile tile)
    {
        if (tile == null)
        {
            Debug.LogError("Se intentó obtener la posición de un tile nulo.");
            return Vector3.zero;
        }
        // Usamos el grid privado para hacer la conversión.
        return grid.CellToWorld(tile.Position);
    }       
}