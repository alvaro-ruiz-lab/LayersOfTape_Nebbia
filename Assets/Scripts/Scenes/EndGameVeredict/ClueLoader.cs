using System.Collections.Generic;
using UnityEngine;

public class ClueLoader : MonoBehaviour
{
    private List<string> inventory;
    [SerializeField] private GameObject CluePrefab;



    void Start()
    {
        inventory = Player.Instance.inventory;
        Player.Instance.gameObject.SetActive(false);
        LoadInventory();
    }

    private void LoadInventory()
    {
        foreach (string clue in inventory)
        {
            UniversalGameController.ItemData.TryGetByName(clue, out var itemData);
            GameObject newItem = Instantiate(CluePrefab, transform);
            var newClue = newItem.GetComponent<Clue>();
            newClue.clueImage.sprite = itemData.itemSprite;
            newClue.clueText.text = itemData.acquittedConclusion;
            newClue.clueName = itemData.itemName;
        }
    }
}
