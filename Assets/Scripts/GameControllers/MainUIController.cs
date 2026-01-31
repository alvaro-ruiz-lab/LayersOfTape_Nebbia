using UnityEngine;

public class MainUIController : MonoBehaviour
{
    // Referencias propias
    [SerializeField] private UIInventoryManager uIInventoryManager;
    [SerializeField] private OxygenBar oxygenBar;
    [SerializeField] private ConversationManager conversationManager;



    // Propiedades
    public static MainUIController Instance { get; private set; }
    public UIInventoryManager UIInventoryManager => Instance.uIInventoryManager;
    public OxygenBar OxygenBar => Instance.oxygenBar;
    public ConversationManager ConversationManager => Instance.conversationManager;



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
}
