using System.Collections.Generic;
using UnityEngine;

public class ClueLoader : MonoBehaviour
{
    [SerializeField] private GameObject CluePrefab;
    [SerializeField] private GameObject SlotPrefab;
    [SerializeField] private GameObject slotsParent;



    void Start()
    {
        LoadInventory();
    }

    private void LoadInventory()
    {
        List<string> inventory = PlayerData.itemsNamesInventory;

        foreach (string clue in inventory)
        {
            UniversalGameController.ItemData.TryGetByName(clue, out var itemData);

            GameObject newSlot = Instantiate(SlotPrefab, slotsParent.transform);

            GameObject newItem = Instantiate(CluePrefab, newSlot.transform);
            Clue newClue = newItem.GetComponent<Clue>();

            newClue.clueImage.sprite = itemData.itemSprite;
            newClue.clueText.text = itemData.acquittedConclusion;
            newClue.clueName = itemData.itemName;
        }
    }
}
