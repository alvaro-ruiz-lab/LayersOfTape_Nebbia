using System;
using AlvaroRuiz.Projects.GameControll.Audio;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class NPC : MonoBehaviour
{
    private static readonly int TertiarySprite = Animator.StringToHash("tertiarySprite");
    private static readonly int SecondaryLaughSprite = Animator.StringToHash("secondaryLaughSprite");
    private static readonly int ChangeSprite = Animator.StringToHash("ChangeSprite");
    [SerializeField] private string npcName;
    private int lineIndex;
    [NonSerialized] public int currentConversationIndex;
    private NPCData npcData;
    [SerializeField] private Sprite secondarySprite;
    [SerializeField] private Sprite tertiarySprite;
    [SerializeField] public int filters = 1;
    [SerializeField] private int stealDifficultyD20 = 13;
    [SerializeField] private bool flirty = true;
    [SerializeField] private bool coward;
    private int stealBonus;
    private int timesStolen;
    private bool laughing;
    [SerializeField] Animator terzaAnimator;
    [SerializeField] AudioClip laughTrack;



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
                Player.Instance.isTalking = true;
                if (!laughing)
                {
                    AudioController.PlaySFX(npcData.npcSound);
                }
            }
            // can only happen if loopConversation is true and the NPC has already said all of their lines
            if (npcData.loopConversation && convIndex == -1)
            {
                convIndex = 0;
            }
            Conversation conv = npcData.conversations[convIndex];
            bool last = lineIndex == conv.lines.Length;
            bool beforeLast = lineIndex == conv.lines.Length - 1;
            bool canSteal = beforeLast && filters > 0;

            if (beforeLast && !npcName.Contains("fake", StringComparison.OrdinalIgnoreCase))
            {
                convItem = conv.itemGiven;
            }
            if (last)
            {
                IncreaseConvIndex();
                MainUIController.ConversationManager.EndConversation();
                if (npcData.isItem)
                {
                    gameObject.SetActive(false);
                }
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
        lineIndex = 0;

        // Speedwagon's last conversation is unlocked by stealing twice.
        if (npcData.isSpeedwagon)
        {
            if (laughing && currentConversationIndex == npcData.conversations.Length - 1)
            {
                ChangeToTerciarySprite();
                currentConversationIndex = -1;
                return;
            }
            if (currentConversationIndex == npcData.conversations.Length - 2)
            {
                currentConversationIndex = -1;
                return;
            }
        }

        if (currentConversationIndex < npcData.conversations.Length - 1)
        {
            currentConversationIndex++;
        }
        else if(currentConversationIndex == npcData.conversations.Length - 1)
        {
            currentConversationIndex = -1;
        }
    }

    public bool Seduce()
    {
        if (npcName.Equals("Litto", StringComparison.OrdinalIgnoreCase))
    {
        MainUIController.ConversationManager.SetConversationText(
            npcData.npcIconSprite,
            "<i>No te juzgo, pero de poco sirve.</i>",
            false
        );
        return false;
    }
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
         if (npcName.Equals("Litto", StringComparison.OrdinalIgnoreCase))
    {
        MainUIController.ConversationManager.SetConversationText(
            npcData.npcIconSprite,
            "<i>Bastante tiene con estar muerto, mejor busca dentro de la mansión.<i>",
            false
        );
        return false;
    }

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
            lineIndex = 0;
            filters--;
            ChangeToSecondaryLaughSprite();
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

    void ChangeToSecondaryLaughSprite()
    {
        laughing = true;
        GetComponent<Animator>().SetTrigger(SecondaryLaughSprite);
        AudioController.PlaySFX(laughTrack);
    }

    void ChangeToTerciarySprite()
    {
        terzaAnimator.SetTrigger(ChangeSprite);
        GetComponent<Animator>().SetTrigger(TertiarySprite);
        laughing = false;
    }
}
