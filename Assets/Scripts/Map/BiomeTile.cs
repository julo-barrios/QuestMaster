using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

// La línea [CreateAssetMenu] nos permitirá crear estos tiles desde el menú de Assets.
[CreateAssetMenu(fileName = "New BiomeTile", menuName = "Tiles/Biome Tile")]
public class BiomeTile : Tile
{
    [Header("Datos del Bioma")]
    public BiomeType biome;
    
    [Tooltip("Lista de posibles enemigos que pueden aparecer en este tile.")]
    public List<string> possibleEnemies;

    // Aquí podrías añadir más datos en el futuro, como:
    // public int movementCost;
    // public string resourceType;
}