using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    /// <summary>
    /// NOMBRE: Gio Catore
    /// </summary>

    // Only item names
    private List<string> inventory = new List<string>();

    // Propiedad
    public static Player Instance { get; private set; }



    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        SetInitPos(new Vector2(0, 0));
    }



    public void SetInitPos(Vector2 position)
    {
        transform.position = position;
    }



    // For tangible items
    void PickUpItem(SceneItem item)
    {
        string name = item.ItemName;
        inventory.Add(name);
        item.gameObject.SetActive(false);
        MainUIController.UIInventoryManager.AddIventoryItem(name);
        // TODO Agregar al Inventario(UI). Crear una instancia del prefab UIItem con el nombre del item.
    }

    // For converasion (items). conversationName must match with InventoryItem.itemName
    void StoreConversation(string conversationName)
    {
        inventory.Add(conversationName);
    }
}
