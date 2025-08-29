// No necesitas "using UnityEngine;" si no usas tipos de Unity como Vector3.
// Pero es buena práctica tenerlo por si en el futuro añades un ícono (Sprite) o algo similar.
using UnityEngine;

// [System.Serializable] es una instrucción para Unity que le dice:
// "Quiero poder ver y editar esta clase en el Inspector".
// Es lo que hace que aparezca de forma ordenada dentro de tu QuestSO.
[System.Serializable]
public class QuestResultData
{
    [Header("Identificación y Narrativa")]
    public string resultLabel = "Éxito"; // Ej: "Éxito Total", "Fracaso con Bajas"
    [TextArea]
    public string resultDescription = "La misión se completó sin problemas."; // Un texto que podría mostrarse al jugador

    [Header("Recompensas y Penalizaciones")]
    public int goldGain = 100;
    public int fameGain = 10;
    public int xpGain = 50;
    public int stressChange = 0; // Un valor positivo para añadir estrés, negativo para reducirlo
    
    // Aquí podrías añadir más cosas en el futuro, como:
    // public List<ItemSO> itemRewards;
    // public int partyEnergyCost = 30;
}