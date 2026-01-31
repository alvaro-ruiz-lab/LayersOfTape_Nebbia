using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NPCData", menuName = "Scriptable Objects/NPCData")]
public class NPCDataList : ScriptableObject
{
    [SerializeField] private List<NPCData> npcList;
    public IReadOnlyList<NPCData> Items => npcList;

    public bool TryGetByName(string name, out NPCData npc)
    {
        npc = null;
        if (string.IsNullOrWhiteSpace(name)) return false;

        for (int i = 0; i < npcList.Count; i++)
        {
            if (string.Equals(npcList[i].name, name, StringComparison.OrdinalIgnoreCase))
            {
                npc = npcList[i];
                return true;
            }
        }
        return false;
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        var seen = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        for (int i = 0; i < npcList.Count; i++)
        {
            var it = npcList[i];
            if (it == null) continue;

            var key = (it.name ?? "").Trim();
            if (key.Length == 0) continue;

            if (!seen.Add(key))
            {
                Debug.LogError($"Duplicate name '{key}' in {name} at index {i}. name must be unique.", this);
            }
        }
    }
#endif
}

[Serializable]
public class NPCData
{
    public string name;
    public bool loopConversation;
    public Sprite npcIconSprite;
    public Conversation[] conversations;
    [NonSerialized] public int currentConversationIndex;

    public void IncreaseConvIndex()
    {
        if (currentConversationIndex < conversations.Length - 1 || loopConversation)
        {
            currentConversationIndex = (currentConversationIndex + 1) % conversations.Length;
        }
        else if(currentConversationIndex == conversations.Length - 1)
        {
            currentConversationIndex = -1;
        }
    }

}

[Serializable]
public class Conversation
{
    public string[] lines;
    public string itemGiven;
}

// 1
    /// <summary>
    /// TestiMone
    /// CHICO
    /// LE ACUSA: Ino Zente
    /// LE EXCULPA: Dio, estaban follando
    /// CARACTERISTICA: Es un pica flor, pero en vd no
    /// </summary>



// 2
    /// <summary>
    /// SpettAtore
    /// CHICO
    /// LE ACUSA: Tria Mitto, porque no le deja hablar de sus piramides
    /// LE EXCULPA: Terza Parte, porque SpettAtore le estaba dando la chapa
    /// CARACTERISTICA: Es un cotilla de cuidao y se cosca de todo
    /// </summary>



// 3
    /// <summary>
    /// InoZente
    /// CHICA
    /// LE ACUSA: La carta que lleva a un camino sin salida, ella misma por ser una cagueta sospechosa, Spett Atore que se cosca que era la ultima en entrar
    /// LE EXCULPA: Las pastis para la depresion
    /// CARACTERISTICA: Es to buena gente pero parece mas sospechosa
    /// </summary>



// 4
    /// <summary>
    /// TerzaParte
    /// CHICA
    /// LE ACUSA: Speedwagon, porque le ha robado el sombrero
    /// LE EXCULPA: Su sombrero, no sale sin el
    /// CARACTERISTICA: Speedwagon le ha robado su sombrero, lo sabe aunque no le haya visto
    /// </summary>



// 5
    /// <summary>
    /// TriaMitto
    /// CHICO
    /// LE ACUSA: Si mismo/a, porque porta la piramide de Yu-gi-oh
    /// LE EXCULPA: La carta A (Tria le cuenta a Signora que se ha jodido la espalda) que dice que ha pasado tal cosa y no ha podido ser el
    /// CARACTERISTICA: Le flipan las piramides
    /// </summary>



// 6
    /// <summary>
    /// Dio
    /// VAMPIRO
    /// LE ACUSA: Speedwagon, y con razon la vd
    /// LE EXCULPA: Testi Mone, estaban follando
    /// CARACTERISTICA:
    ///     - Best villano
    ///     - Best bisexual
    ///     - Best... esperabas otro argumento, pero era yo, DIO!
    /// </summary>



// 7
    /// <summary>
    /// Speedwagon
    /// BESTO WAIFU EVER
    /// LE ACUSA: Terza Parte, por robarle el sombrero
    /// LE EXCULPA: El sombrero de Terza Parte, se lo estaba robando (se descubre robandole 2 veces)
    /// CARACTERISTICA:
    ///     - Besto waifu ever
    ///     - Besto frendo te regala mascarilla a full
    /// </summary>
