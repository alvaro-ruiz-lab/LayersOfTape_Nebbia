using AlvaroRuiz.Projects.GameControll.Audio;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    /// <summary>
    /// NOMBRE: Gio Catore
    /// </summary>
    ///

    // VARIABLES/CAMPOS-------------------------------------------------------------
    // Referencias propias
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private Rigidbody2D rigidBody;
    [SerializeField] private Animator animator;
    [SerializeField] private Oxygen oxygen;
    [SerializeField] private Transform triggerRotator;

    // INPUTS SISTEM VARIABLES
    [Header("Input and config")]
    [SerializeField] private float movementSpeed;
    private bool currentInput;
    private Vector2 inputDirection;
    private Vector2 lastInputDirection;

    [NonSerialized] public NPC talkableNPC;
    [NonSerialized] public bool isTalking;
    [NonSerialized] public bool canVeredict;

    // SFX
    [Header("SFX")]
    [SerializeField] private AudioClip walkClip;

    // Propiedades
    public static Player Instance { get; private set; }
    public static PlayerInput PI { get { return Instance.playerInput; } }
    public static Oxygen Oxygen => Instance.oxygen;



    // METODOS DE UNITY-------------------------------------------------------------
    // METODOS DE INIT DE UNITY
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            return;
        }
        Destroy(gameObject);
    }

    private void Start()
    {
        // Init pos en cada escena
        transform.position = new Vector2(0, 0);
    }



    // METODOS DE UPDATEO DE UNITY
    void Update()
    {
        // Chequear si se esta moviendo
        currentInput = inputDirection != Vector2.zero ? true : false;
    }

    void FixedUpdate()
    {
        MovePlayer();
    }



    // HELPERS-------------------------------------------------------------
    // MOVEMENT
    void MovePlayer()
    {
        if (currentInput && !isTalking)
        {
            // Activar animacion de movimiento y sonido
            animator.SetTrigger("Move");
            AudioController.PlaySound(walkClip);

            // Mover personaje
            var direction = inputDirection.normalized;
            Vector3 movement = direction * movementSpeed * Time.fixedDeltaTime;
            transform.position += (movement * Time.fixedDeltaTime * movementSpeed);

            // Cambiar direccion del sprite
            ReorientPlayer();

            // Rotar trigger de interaccion
            if (inputDirection.sqrMagnitude > 0.0001f)
            {
                triggerRotator.rotation = Quaternion.AngleAxis(Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90f, Vector3.forward);
            }

            return;
        }

        // Detener animacion de movimiento
        animator.SetTrigger("Stop");
    }

    private void ReorientPlayer()
    {
        bool changeXDirection = Math.Sign(lastInputDirection.x) != Math.Sign(inputDirection.x);

        int properHorizontalValue = Math.Sign(inputDirection.x) < 0 ? 1 : Math.Sign(inputDirection.x);
        animator.SetFloat("Horizontal", properHorizontalValue);
        animator.SetFloat("Vertical", Math.Sign(inputDirection.y));

        if (changeXDirection)
        {
            spriteRenderer.flipX = inputDirection.x < 0 ? true : inputDirection.x > 0 ? false : spriteRenderer.flipX;
            lastInputDirection = inputDirection;
        }
    }



    // INTERACTIONS
    // For tangible items
    void PickUpItem(SceneItem item)
    {
        string name = item.ItemName;
        StoreConversation(name);
        item.gameObject.SetActive(false);
        MainUIController.UIInventoryManager.AddIventoryItem(name);
        // TODO Agregar al Inventario(UI). Crear una instancia del prefab UIItem con el nombre del item.
    }

    private void TalkToNPC(NPC npc)
    {
        var conversationItem = npc.Talk();
        if (!string.IsNullOrWhiteSpace(conversationItem)) StoreConversation(conversationItem);
    }

    // For converasion (items). conversationName must match with InventoryItem.itemName
    void StoreConversation(string name)
    {
        PlayerData.itemsNamesInventory.Add(name);
    }



    // INPUT SYSTEM-------------------------------------------------------------
    private void OnMove(InputValue value)
    {
        inputDirection = value.Get<Vector2>();
    }



    private void OnInteract()
    {
        //Debug.Log("Interacted");
        if (isTalking)
        {
            talkableNPC.Talk();
        }
        else if (talkableNPC)
        {
            TalkToNPC(talkableNPC);
        }
        else if (canVeredict)
        {
            StartCoroutine(LoadVeredict());
        }
    }

    private IEnumerator LoadVeredict()
    {
        AudioController.PlayInitMusic();
        yield return new WaitForSeconds(2.5f);

        Debug.Log("Loading Veredict Scene...");

        SceneManager.LoadScene("EndGameVeredict");
    }
}
