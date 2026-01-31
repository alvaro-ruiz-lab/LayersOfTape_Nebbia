using UnityEngine;
using UnityEngine.UI;

public class UIItem : MonoBehaviour
{
    [SerializeField] private string itemName;
    public string ItemName => itemName;
    void Start()
    {
        UniversalGameController.ItemData.TryGetByName(itemName, out var itemData);
        GetComponent<Image>().sprite = itemData.itemSprite;
    }
}
