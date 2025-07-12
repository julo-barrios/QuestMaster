using UnityEngine;
using TMPro;
using UnityEngine.UI; // No olvidar para el texto

public class TopBarTavernUI : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI goldText;
    
    [SerializeField] private TextMeshProUGUI rankText;
    [SerializeField] private Slider rankslider;

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
        rankslider.maxValue = GuildManager.Instance.MaxFame;
        rankslider.value = GuildManager.Instance.CurrentFame;
        rankText.text = GuildManager.Instance.currentRank.ToString();
    }

    // El método que reacciona al evento.
    private void UpdateGoldText(int newAmount)
    {
        goldText.text = newAmount.ToString();
    }
}