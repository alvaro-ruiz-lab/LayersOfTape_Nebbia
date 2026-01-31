using UnityEngine;

public class SceneItem : MonoBehaviour
{
    [SerializeField] private string itemName;
    public string ItemName => itemName;
    void Start()
    {
        UniversalGameController.ItemData.TryGetByName(itemName, out var itemData);
        GetComponent<SpriteRenderer>().sprite = itemData.itemSprite;
    }
}
