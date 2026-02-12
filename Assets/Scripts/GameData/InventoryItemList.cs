using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "InventoryItemList", menuName = "Scriptable Objects/InventoryItemList")]
public class InventoryItemList : ScriptableObject
{
    [SerializeField] private List<InventoryItem> items;
    public IReadOnlyList<InventoryItem> Items => items;



    public bool TryGetByName(string name, out InventoryItem item)
    {
        item = null;
        if (string.IsNullOrWhiteSpace(name)) return false;

        for (int i = 0; i < items.Count; i++)
        {
            if (string.Equals(items[i].itemName, name, StringComparison.OrdinalIgnoreCase))
            {
                item = items[i];
                return true;
            }
        }
        return false;
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        var seen = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        for (int i = 0; i < items.Count; i++)
        {
            var it = items[i];
            if (it == null) continue;

            var key = (it.itemName ?? "").Trim();
            if (key.Length == 0) continue;

            if (!seen.Add(key))
            {
                Debug.LogError($"Duplicate itemName '{key}' in {name} at index {i}. itemName must be unique.", this);
            }
        }
    }
#endif
}

[Serializable]
public class InventoryItem
{
    public string itemName;
    [TextArea(1, 3)]
    public string acquittedConclusion;
    public string associatedNPC;
    public bool good = true;
    public bool showInInventory;
    public Sprite itemSprite;
}
