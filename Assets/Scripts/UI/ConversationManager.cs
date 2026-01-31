using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ConversationManager : MonoBehaviour
{
    // Referencias propias
    [SerializeField] private Image conversationBg;
    [SerializeField] private TextMeshProUGUI conversationText;
    [SerializeField] private GameObject ActionsButtonsPanel;
    [SerializeField] private Image speakerImg;

    // Propiedades
    public static ConversationManager Instance { get; private set; }



    public void SetConversationText(string text)
    {
        conversationText.text = text;
    }
}