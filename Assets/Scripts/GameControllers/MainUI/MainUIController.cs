using UnityEngine;
using UnityEngine.SceneManagement;

public class MainUIController : MonoBehaviour
{
    // Referencias propias
    [SerializeField] private UIInventoryManager uIInventoryManager;
    [SerializeField] private OxygenBar oxygenBar;
    [SerializeField] private ConversationManager conversationManager;
    [SerializeField] private GameObject gameOverPanel;



    // Propiedades
    public static MainUIController Instance { get; private set; }
    public static UIInventoryManager UIInventoryManager => Instance.uIInventoryManager;
    public static OxygenBar OxygenBar => Instance.oxygenBar;
    public static ConversationManager ConversationManager => Instance.conversationManager;



    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }



    public void ShowGameOverScreen()
    {
        gameOverPanel.SetActive(true);
    }

    public void RestartGame()
    {
        // TODO limpiar inventario
        // TODO resetear dialogos NPC
        // TODO reponer items?
        gameOverPanel.SetActive(false);
        Player.Oxygen.RefillOxygen(100);
        SceneManager.LoadScene("Start");
    }
}
