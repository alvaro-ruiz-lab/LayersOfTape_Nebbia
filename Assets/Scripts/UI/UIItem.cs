using UnityEngine;
using UnityEngine.UI;

public class UIItem : MonoBehaviour
{
    public void SetItemData (string name)
    {
        UniversalGameController.ItemData.TryGetByName(name, out var itemData);
        GetComponent<Image>().sprite = itemData.itemSprite;
    }
}
