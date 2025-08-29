using System.Collections.Generic;
using UnityEngine;

// Definimos los tipos de biomas para no usar strings
public enum BiomeType { Plains, Forest }
// Podrías tener una clase o SO para Faction, pero empecemos simple
public enum Faction { Player, Goblins, Undead }

public class HexTile
{
    // Coordenadas del tile en el grid
    public Vector3Int Position { get; private set; }
    
    // Propiedades del tile
    public BiomeType Biome { get; set; }
    // Podrías usar ScriptableObjects para los enemigos para más flexibilidad
    public List<string> PossibleEnemies { get; set; }
    
    // Sistema de influencia: qué facción tiene cuánta influencia (de 0 a 1)
    public Dictionary<Faction, float> Influence { get; private set; }
    public bool HasQuest { get; internal set; }

    // Constructor para crear un nuevo tile
    public HexTile(Vector3Int position, BiomeType biome)
    {
        this.Position = position;
        this.Biome = biome;
        this.PossibleEnemies = new List<string>();
        this.Influence = new Dictionary<Faction, float>();
    }
}