using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Velocidad de Movimiento")]
    public float moveSpeed = 50f;

    [Tooltip("La coordenada X del borde más a la izquierda del mapa.")]
    public float mapMinX = -100f;
    [Tooltip("La coordenada X del borde más a la derecha del mapa.")]
    public float mapMaxX = 100f;
    [Tooltip("La coordenada Y del borde más abajo del mapa.")]
    public float mapMinY = -100f;
    [Tooltip("La coordenada Y del borde más arriba del mapa.")]
    public float mapMaxY = 100f;

    [Header("Límites del Zoom")]
    public float zoomSpeed = 25f;
    public float minZoom = 5f;  // Vista más cercana
    public float maxZoom = 50f; // Vista más alejada

    private Camera cam;

    void Start()
    {
        // Obtenemos el componente Camera para poder cambiar su tamaño ortográfico.
        cam = GetComponent<Camera>();
    }

    void LateUpdate()
    {
        HandleMovement();
        HandleZoom();
    }

    private void HandleMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 moveDirection = new Vector3(horizontalInput, verticalInput, 0);
        
        Vector3 newPosition = transform.position + moveDirection * moveSpeed * Time.deltaTime;

        float camHeight = cam.orthographicSize;
        float camWidth = cam.orthographicSize * cam.aspect;

        float effectiveMinX = mapMinX + camWidth;
        float effectiveMaxX = mapMaxX - camWidth;
        float effectiveMinY = mapMinY + camHeight;
        float effectiveMaxY = mapMaxY - camHeight;

        // --- ¡AQUÍ ESTÁ LA CORRECCIÓN CRÍTICA! ---
        // Si la cámara es más ancha que el mapa, los límites se invierten.
        // En ese caso, en lugar de sujetar la posición, la centramos.
        if (effectiveMinX > effectiveMaxX)
        {
            newPosition.x = (mapMinX + mapMaxX) / 2;
        }
        else
        {
            // Si no, aplicamos los límites normalmente.
            newPosition.x = Mathf.Clamp(newPosition.x, effectiveMinX, effectiveMaxX);
        }

        // Hacemos lo mismo para el eje Y.
        if (effectiveMinY > effectiveMaxY)
        {
            newPosition.y = (mapMinY + mapMaxY) / 2;
        }
        else
        {
            newPosition.y = Mathf.Clamp(newPosition.y, effectiveMinY, effectiveMaxY);
        }
        // ------------------------------------

        transform.position = newPosition;
    }
    
    private void HandleZoom()
    {
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");
        float newZoom = cam.orthographicSize - scrollInput * zoomSpeed;
        cam.orthographicSize = Mathf.Clamp(newZoom, minZoom, maxZoom);
    }
}