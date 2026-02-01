using System;
using AlvaroRuiz.Projects.GameControll.Audio;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class NPC : MonoBehaviour
{
    [SerializeField] private string npcName;
    private int lineIndex;
    private NPCData npcData;
    [SerializeField] private Sprite secondarySprite;
    [SerializeField] public int filters = 1;
    [SerializeField] private int stealDifficultyD20 = 11;
    [SerializeField] private bool flirty = true;
    [SerializeField] private bool coward = false;
    private int stealBonus;
    private int timesStolen;
    [NonSerialized] public int currentConversationIndex;



    void Start()
    {
        UniversalGameController.NPCData.TryGetByName(npcName, out npcData);
    }

    public string Talk()
    {
        var convIndex = currentConversationIndex;
        string convItem = "";
        if (convIndex >= 0 && convIndex < npcData.conversations.Length || npcData.loopConversation)
        {
            if (lineIndex == 0)
            {
                AudioController.PlaySFX(npcData.npcSound);
                Player.Instance.isTalking = true;
            }
            // can only happen if loopConversation is true and the NPC has already said all of their lines
            if (convIndex == -1)
            {
                convIndex = 0;
            }
            Conversation conv = npcData.conversations[convIndex];
            bool last = lineIndex == conv.lines.Length;
            bool beforeLast = lineIndex == conv.lines.Length - 1;
            bool canSteal = beforeLast && filters > 0;
            convItem = conv.itemGiven;

            if (beforeLast && convIndex == npcData.conversations.Length - 1 && npcData.isSpeedwagon)
            {
                Player.Oxygen.RefillOxygen(100);
            }

            if (last)
            {
                lineIndex = 0;
                IncreaseConvIndex();
                MainUIController.ConversationManager.EndConversation();
            }
            else
            {
                MainUIController.ConversationManager.SetConversationText(npcData.npcIconSprite, conv.lines[lineIndex], canSteal);
                lineIndex++;
            }
        }
        return convItem;
    }

    private void IncreaseConvIndex()
    {
        if (currentConversationIndex < npcData.conversations.Length - 1)
        {
            currentConversationIndex++;
        }
        else if(currentConversationIndex == npcData.conversations.Length - 1)
        {
            currentConversationIndex = -1;
        }

        // Speedwagon's last conversation is unlocked by stealing twice.
        if (npcData.isSpeedwagon && filters == 2 && currentConversationIndex == npcData.conversations.Length - 1)
        {
            currentConversationIndex = -1;
        }
    }

    public bool Seduce()
    {
        if (flirty)
        {
            stealBonus += 2;
            MainUIController.ConversationManager.SetConversationText(npcData.npcIconSprite, "Te veo con otros ojos...", true, true);
            return true;
        }
        stealBonus -= 2;
        MainUIController.ConversationManager.SetConversationText(npcData.npcIconSprite, "¡Ni en sueños!", false);
        return false;
    }

    public bool Threaten()
    {
        if (coward)
        {
            stealBonus += 2;
            MainUIController.ConversationManager.SetConversationText(npcData.npcIconSprite, "Perdón, no quiero problemas.", true, true);
            return true;
        }
        stealBonus -= 2;
        MainUIController.ConversationManager.SetConversationText(npcData.npcIconSprite, "¿Tú y cuántos más?", false);
        return false;
    }

    public int Steal()
    {
        if (filters <= 0) return 0;
        timesStolen++;
        if (timesStolen == 2 && npcData.isSpeedwagon)
        {
            currentConversationIndex = npcData.conversations.Length - 1;
            filters--;
            ChangeToSecondarySprite();
            return 100;
        }

        int result = Random.Range(1, 21) + stealBonus;
        if (result >= stealDifficultyD20)
        {
            filters--;
            result = math.clamp(result * 5, 50, 90);
            MainUIController.ConversationManager.SetConversationText(null, $"<i>CONSEGUISTE UN FILTRO AL {result}%</i>", false);
            return result;
        }
        MainUIController.ConversationManager.SetConversationText(npcData.npcIconSprite, "¡Un poco de respeto!", false);
        stealBonus -= 1;
        return 0;
    }

    void ChangeToSecondarySprite()
    {
        GetComponent<SpriteRenderer>().sprite = secondarySprite;
    }
}
