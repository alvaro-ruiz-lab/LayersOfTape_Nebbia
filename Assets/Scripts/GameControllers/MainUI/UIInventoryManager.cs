using UnityEngine;

public class UIInventoryManager : MonoBehaviour
{
    [SerializeField] private GameObject UIItemPrefab;



    public void AddIventoryItem(string newItemName)
    {
        if (UniversalGameController.ItemData.TryGetByName(newItemName, out var itemData) && itemData.showInInventory)
        {
            GameObject newItem = Instantiate(UIItemPrefab, transform);
            newItem.GetComponent<UIItem>().SetItemData(newItemName);
        }
    }
}
