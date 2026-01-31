using UnityEngine;

public class UIInventoryManager : MonoBehaviour
{
    [SerializeField] private GameObject UIItemPrefab;



    public void AddIventoryItem(string newItemName)
    {
        GameObject newItem = Instantiate(UIItemPrefab, transform);
        newItem.GetComponent<UIItem>().SetItemData(newItemName);
    }
}
