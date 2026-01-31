using UnityEngine;

public class NPC : MonoBehaviour
{
    [SerializeField] private string name;
    private int lineIndex;
    private NPCData npcData;



    void Start()
    {
        UniversalGameController.NPCData.TryGetByName(name, out npcData);
    }

    public string Talk()
    {
        lineIndex = 0;
        return AdvanceConversation();
    }

    public string AdvanceConversation()
    {
        var convIndex = npcData.currentConversationIndex;
        string convItem = "";
        if (convIndex >= 0 && convIndex <= npcData.conversations.Length)
        {
            Conversation conv = npcData.conversations[convIndex];
            string text = conv.lines[lineIndex];
            bool last = lineIndex == conv.lines.Length - 1;
            MainUIController.ConversationManager.SetConversationText(npcData.npcIconSprite, text, last);
            convItem = conv.itemGiven;

            if (last)
            {
                lineIndex = 0;
                npcData.IncreaseConvIndex();
            }
            else
            {
                lineIndex++;
            }
        }
        return convItem;
    }
}
