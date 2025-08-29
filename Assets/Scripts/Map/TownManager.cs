using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TownManager : MonoBehaviour
{
    public List<Tile> TownTiles { get; set; }
    public int Population { get; set; } = 100;
    public float Happiness { get; set; } = 1.0f; // 0–1
    public void UpdateTownState() {
        // lógica de crecimiento, cambios visuales, etc.
    }
}