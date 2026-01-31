using System.Collections.Generic;
using UnityEngine;

public class InventoryItem
{
    public string itemName;
    public string acquittedConclusion;
    public NPC associatedNPC;
    public bool showOnInventory;
    public Sprite itemSprite;

    public InventoryItem(string name, string conclusio, NPC npc, bool tangible, Sprite itemImg)
    {
        itemName = name;
        acquittedConclusion = conclusio;
        associatedNPC = npc;
        showOnInventory = tangible; // Para decicir si se pone en UI de inventario o no
        itemSprite = itemImg;
    }
}



public class NPC
{
    public string name;
    public bool enableLoop;

    public NPC(string npcName, bool loop)
    {
        name = npcName;
        enableLoop = loop;
    }
}


public static class NPCcData
{
    public static NPC TestiMone = new NPC(
        "TestiMone",
        false
    );
}



public static class PlayerData
{
    /// <summary>
    /// NOMBRE: Gio Catore
    /// </summary>

    private static List<InventoryItem> inventory = new List<InventoryItem>();

}
