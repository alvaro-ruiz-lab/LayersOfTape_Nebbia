using System.Collections.Generic;
using UnityEngine;




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

    private static List<InventoryItemList> inventory = new List<InventoryItemList>();

}
