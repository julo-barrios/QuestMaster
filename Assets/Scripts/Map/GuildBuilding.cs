using UnityEngine;

public class GuildBuilding : MonoBehaviour
{
    public string buildingName = "Gremio de Aventureros";
    public int level = 1;
    // Otras propiedades: mejoras, capacidad, etc.

    // Este método se activa cuando haces clic sobre el edificio
    // si tiene un componente Collider2D.
    private void OnMouseDown()
    {
        Debug.Log($"Has hecho clic en {buildingName}, Nivel {level}.");
        // Aquí podrías abrir el menú de gestión del gremio.
        // UIManager.Instance.OpenGuildMenu();
    }
}