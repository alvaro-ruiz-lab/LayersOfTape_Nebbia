using TMPro;
using UnityEngine;

public class ConversationManager : MonoBehaviour
{
    // Referencias propias
    [SerializeField] private TextMeshProUGUI conversationText;

    // Propiedades
    public static ConversationManager Instance { get; private set; }



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



    public void SetConversationText(string text)
    {
        conversationText.text = text;
    }
}