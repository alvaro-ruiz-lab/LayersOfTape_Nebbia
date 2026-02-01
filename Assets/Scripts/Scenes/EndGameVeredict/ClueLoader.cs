using System.Collections.Generic;
using UnityEngine;

public class ClueLoader : MonoBehaviour
{
    private List<string> inventory;
    [SerializeField] private GameObject CluePrefab;
    [SerializeField] private GameObject SlotPrefab;
    [SerializeField] private List<Slot> slotList;
    private int slotCount;



    void Awake()
    {
        inventory = Player.Instance.inventory;
        LoadInventory();
    }

    private void LoadInventory()
    {
        foreach (string clue in inventory)
        {
            UniversalGameController.ItemData.TryGetByName(clue, out var itemData);
            GameObject newSlot = Instantiate(SlotPrefab, transform);
            GameObject newItem = Instantiate(CluePrefab, newSlot.transform);
            var newClue = newItem.GetComponent<Clue>();
            newClue.clueImage.sprite = itemData.itemSprite;
            newClue.clueText.text = itemData.acquittedConclusion;
            newClue.clueName = itemData.itemName;
        }
    }
}
