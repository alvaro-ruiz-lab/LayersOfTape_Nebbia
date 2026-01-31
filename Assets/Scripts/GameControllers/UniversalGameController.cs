using System.Collections.Generic;
using UnityEngine;

public class UniversalGameController : MonoBehaviour
{
    // Singleton
    public static UniversalGameController Instance { get; private set; }
    [SerializeField] private InventoryItemList itemData;
    public static InventoryItemList ItemData => Instance.itemData;



    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
    }



    void Update()
    {
        
    }
}
