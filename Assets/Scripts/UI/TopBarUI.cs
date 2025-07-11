using UnityEngine;
using TMPro; // No olvidar para el texto

public class TopBarUI : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI goldText;
    // (Puedes añadir aquí las referencias a tu slider de Fama, texto de Nivel, etc. en el futuro)

    // Este método se ejecuta cuando el objeto se activa.
    private void OnEnable()
    {
        // Nos suscribimos al evento. Ahora, cada vez que GuildManager.OnGoldChanged se dispare,
        // se llamará a nuestro método UpdateGoldText.
        GuildManager.OnGoldChanged += UpdateGoldText;
    }

    // Este método se ejecuta cuando el objeto se desactiva.
    private void OnDisable()
    {
        // Es MUY IMPORTANTE desuscribirse para evitar errores y fugas de memoria.
        GuildManager.OnGoldChanged -= UpdateGoldText;
    }

    void Start()
    {
        goldText.text = GuildManager.Instance.currentGold.ToString();
    }

    // El método que reacciona al evento.
    private void UpdateGoldText(int newAmount)
    {
        goldText.text = newAmount.ToString();
    }
}