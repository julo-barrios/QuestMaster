using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

// La línea [CreateAssetMenu] nos permitirá crear estos tiles desde el menú de Assets.
[CreateAssetMenu(fileName = "New RoadTile", menuName = "Tiles/Road Tile")]
public class RoadTile : Tile
{

    // Aquí podrías añadir más datos en el futuro, como:
    // public int movementCost;
    // public string resourceType;
}