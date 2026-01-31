using UnityEngine;

public class NPC : MonoBehaviour
{
    [SerializeField] private string npcName;
    private int lineIndex;
    private NPCData npcData;



    void Start()
    {
        UniversalGameController.NPCData.TryGetByName(npcName, out npcData);
    }

    public string Talk()
    {
        var convIndex = npcData.currentConversationIndex;
        string convItem = "";
        if (convIndex >= 0 && convIndex < npcData.conversations.Length)
        {
            if (lineIndex == 0)
            {
                Player.Instance.isTalking = true;
            }
            Conversation conv = npcData.conversations[convIndex];
            bool last = lineIndex == conv.lines.Length;
            bool beforeLast = lineIndex == conv.lines.Length - 1;
            convItem = conv.itemGiven;

            if (last)
            {
                lineIndex = 0;
                npcData.IncreaseConvIndex();
                MainUIController.ConversationManager.EndConversation();
            }
            else
            {
                MainUIController.ConversationManager.SetConversationText(npcData.npcIconSprite, conv.lines[lineIndex], beforeLast);
                lineIndex++;
            }
        }
        return convItem;
    }
}
