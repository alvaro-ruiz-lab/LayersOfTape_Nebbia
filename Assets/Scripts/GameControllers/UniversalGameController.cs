using System.Collections.Generic;
using UnityEngine;

public class UniversalGameController : MonoBehaviour
{
    // Singleton
    public static UniversalGameController Instance { get; private set; }
    [SerializeField] private InventoryItemList itemData;
    [SerializeField] private NPCDataList npcData;
    public static InventoryItemList ItemData => Instance.itemData;
    public static NPCDataList NPCData => Instance.npcData;



    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
