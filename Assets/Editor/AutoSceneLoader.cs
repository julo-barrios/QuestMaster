using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

// Este atributo le dice a Unity que ejecute el constructor estático de esta clase
// cada vez que se carga el editor o se compila el código.
[InitializeOnLoad]
public static class AutoSceneLoader
{
    // La ruta a tu escena de inicialización. ¡Asegúrate de que sea correcta!
    private static readonly string initializationScenePath = "Assets/Scenes/Initialization.unity";

    // Variables para recordar en qué escena estábamos trabajando.
    private static string previousScenePath;
    private static bool wasPlaying = false;

    // Constructor estático que se ejecuta una sola vez.
    static AutoSceneLoader()
    {
        // Nos suscribimos al evento que nos notifica los cambios de estado del modo de juego.
        EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
    }

    private static void OnPlayModeStateChanged(PlayModeStateChange state)
    {
        // SE EJECUTA JUSTO ANTES DE ENTRAR EN MODO PLAY
        if (state == PlayModeStateChange.ExitingEditMode)
        {
            // Guardamos la escena actual para poder volver a ella después.
            previousScenePath = EditorSceneManager.GetActiveScene().path;

            // Si la escena actual NO es la de inicialización, la cargamos.
            if (previousScenePath != initializationScenePath)
            {
                // Guardamos cualquier cambio pendiente en la escena actual.
                if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
                {
                    // Cargamos la escena de inicialización.
                    EditorSceneManager.OpenScene(initializationScenePath);
                    wasPlaying = true;
                }
                else
                {
                    // Si el usuario cancela el guardado, cancelamos el modo Play.
                    EditorApplication.isPlaying = false;
                }
            }
        }
        // SE EJECUTA JUSTO AL SALIR DEL MODO PLAY
        else if (state == PlayModeStateChange.EnteredEditMode)
        {
            // Si habíamos cargado la escena de inicialización automáticamente,
            // volvemos a la escena en la que estábamos trabajando.
            if (wasPlaying && !string.IsNullOrEmpty(previousScenePath))
            {
                EditorSceneManager.OpenScene(previousScenePath);
            }
            wasPlaying = false;
        }
    }
}
