// SceneLoader.cs
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    void Start()
    {
        // Carga tu menú principal o la primera escena del juego.
        SceneManager.LoadScene("MapScene"); // Cambiá "MainMenuScene" por el nombre de tu escena.
    }
}
