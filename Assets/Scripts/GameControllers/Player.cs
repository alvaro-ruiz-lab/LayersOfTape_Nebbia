using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    /// <summary>
    /// NOMBRE: Gio Catore
    /// </summary>
    ///

    // Referencias propias
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private Animator animator;

    // INPUTS SISTEM VARIABLES
    [Header("Input")]
    [SerializeField] private float speed = 5f;
    private Vector3 inputDirection;
    private bool currentInput;
    private Vector2 lastInputDirection;

    // Only item names
    private List<string> inventory = new List<string>();

    // Propiedades
    public static Player Instance { get; private set; }
    public static PlayerInput PI { get { return Instance.playerInput; } }



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



    void Update()
    {
        // Chequear si se esta moviendo
        currentInput = inputDirection != Vector3.zero ? true : false;
    }

    void FixedUpdate()
    {
        MovePlayer();
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



    // INPUT SYSTEM-------------------------------------------------------------
    private void OnMove(InputValue value)
    {
        inputDirection = value.Get<Vector2>();
    }

    void MovePlayer()
    {
        if (currentInput == true)
        {
            animator.SetTrigger("Move");

            Vector3 movement = inputDirection.normalized * speed * Time.deltaTime;
            transform.position += (movement * Time.deltaTime * speed);

            bool changeDirection = lastInputDirection.x != inputDirection.x;
            if (changeDirection)
            {
                spriteRenderer.flipX = inputDirection.x < 0 ? true : inputDirection.x > 0 ? false : spriteRenderer.flipX;
                lastInputDirection = inputDirection;
            }

            return;
        }

        animator.SetTrigger("Stop");
    }



    private void OnInteract()
    {

        // Aqui creo que deberiamos chequear triggers de personajes o de objetos
        Debug.Log("Interacted");
    }

    void TalkToNPC(NPC npc)
    {
        var conversationItem = npc.Talk();
        if (!string.IsNullOrWhiteSpace(conversationItem)) StoreConversation(conversationItem);
    }
}
