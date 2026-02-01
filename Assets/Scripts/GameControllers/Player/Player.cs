using System;
using System.Collections.Generic;
using AlvaroRuiz.Projects.GameControll.Audio;
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
    [SerializeField] private Rigidbody2D rigidBody;
    [SerializeField] private Animator animator;
    [SerializeField] private Oxygen oxygen;
    [SerializeField] private Transform triggerRotator;

    // INPUTS SISTEM VARIABLES
    [Header("Input")]
    [SerializeField] private float movementSpeed;
    private bool currentInput;
    private Vector2 inputDirection;
    private Vector2 lastInputDirection;

    [NonSerialized] public NPC talkableNPC;
    [NonSerialized] public bool isTalking;

    // SFX
    [Header("SFX")]
    [SerializeField] private AudioClip walkClip;

    // Only item names
    private List<string> inventory = new List<string>();

    // Propiedades
    public static Player Instance { get; private set; }
    public static PlayerInput PI { get { return Instance.playerInput; } }
    public static Oxygen Oxygen => Instance.oxygen;


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
        currentInput = inputDirection != Vector2.zero ? true : false;
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
        if (currentInput && !isTalking)
        {
            // Activar animacion de movimiento y sonido
            animator.SetTrigger("Move");
            AudioController.PlaySound(walkClip);

            // Mover personaje
            var direction = inputDirection.normalized;
            Vector3 movement = direction * movementSpeed * Time.deltaTime;
            transform.position += (movement * Time.deltaTime * movementSpeed);

            // Cambiar direccion del sprite en horizontal
            bool changeXDirection = Math.Sign(lastInputDirection.x) != Math.Sign(inputDirection.x);

            int properHorizontalValue = Math.Sign(inputDirection.x) < 0 ? 1 : Math.Sign(inputDirection.x);
            animator.SetFloat("Horizontal", properHorizontalValue);
            animator.SetFloat("Vertical", Math.Sign(inputDirection.y));

            if (changeXDirection)
            {
                spriteRenderer.flipX = inputDirection.x < 0 ? true : inputDirection.x > 0 ? false : spriteRenderer.flipX;
                lastInputDirection = inputDirection;
            }

            // Rotar trigger de interaccion
            if (inputDirection.sqrMagnitude > 0.0001f)
            {
                triggerRotator.rotation = Quaternion.AngleAxis(Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg +90f, Vector3.forward);
            }

            return;
        }

        // Detener animacion de movimiento
        animator.SetTrigger("Stop");
    }



    private void OnInteract()
    {
        Debug.Log("Interacted");
        if (isTalking)
        {
            talkableNPC.Talk();
        }
        else if (talkableNPC)
        {
            TalkToNPC(talkableNPC);
        }
    }

    private void TalkToNPC(NPC npc)
    {
        var conversationItem = npc.Talk();
        if (!string.IsNullOrWhiteSpace(conversationItem)) StoreConversation(conversationItem);
    }
}
