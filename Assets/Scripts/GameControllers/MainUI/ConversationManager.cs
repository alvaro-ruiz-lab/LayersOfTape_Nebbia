using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ConversationManager : MonoBehaviour
{
    // Referencias propias
    [SerializeField] private Image conversationBg;
    [SerializeField] private TextMeshProUGUI conversationText;
    [SerializeField] private GameObject actionsButtonsPanel;
    [SerializeField] private Image speakerImg;



    public void SetGameContextInfo(string text)
    {
        conversationBg.color = new Color(0, 0, 0, 0.75f);

        conversationText.color = Color.white;
        conversationText.text = text;

        actionsButtonsPanel.SetActive(false);

        speakerImg.gameObject.SetActive(false);
    }



    public void SetConversationText(Sprite speakerFace, string text, bool readyToAction)
    {
        conversationBg.color = Color.white;

        conversationText.color = Color.black;
        conversationText.text = text;

        actionsButtonsPanel.SetActive(readyToAction);

        speakerImg.gameObject.SetActive(true);
        if (speakerImg.sprite != speakerFace)
            speakerImg.sprite = speakerFace;
    }



    public void EndConversation()
    {
        gameObject.SetActive(false);
    }
}