using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ConversationManager : MonoBehaviour
{
    // Referencias propias
    [SerializeField] private Image conversationBg;
    [SerializeField] private TextMeshProUGUI conversationText;
    [SerializeField] private GameObject actionsButtonsPanel;
    [SerializeField] private GameObject flirtButton;
    [SerializeField] private GameObject threatenButton;
    [SerializeField] private Image speakerImg;
    [NonSerialized] public bool waitingNoSkip;



    public void SetConversationText(Sprite speakerFace, string text, bool readyToAction, bool stealOnly = false)
    {
        if (waitingNoSkip) return;

        gameObject.SetActive(true);

        bool isAConversation = speakerFace != null; // Si no hay imagen de hablante, es solo texto

        conversationBg.color = isAConversation ? Color.white : new Color(0, 0, 0, 0.75f);

        conversationText.color = isAConversation ? Color.black : Color.white;
        conversationText.text = text;

        actionsButtonsPanel.SetActive(readyToAction);
        flirtButton.SetActive(!stealOnly);
        threatenButton.SetActive(!stealOnly);

        speakerImg.gameObject.SetActive(isAConversation);
        if (isAConversation && speakerImg.sprite != speakerFace)
            speakerImg.sprite = speakerFace;
    }

    public bool IsConversationActive() => actionsButtonsPanel.activeSelf;


    public void EndConversation()
    {
        Player.Instance.isTalking = false;
        gameObject.SetActive(false);
    }
}
