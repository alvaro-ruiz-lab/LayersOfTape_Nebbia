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
    [SerializeField] private AudioClip interactClip;
    [SerializeField] private AudioClip cancelClip;

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

  //SPAWN DE PLAYER EN CADA ESCENA
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        var spawn = GameObject.FindGameObjectWithTag("PlayerSpawn");
        if (spawn != null)
        {
            transform.position = spawn.transform.position;
        }

        // Cambiar audio ambiental según la escena
        AudioController.SetEnvironmentAudioForScene(scene.name);
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
            AudioController.PlaySFX(walkClip);

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
    private void TalkToNPC(NPC npc)
    {
        StoreItem(npc.Talk());
    }

    // For converasion (items). conversationName must match with InventoryItem.itemName
    // Supports refilling oxygen with the format oxygen+50
    // When NPC gives an item and oxygen, itemName is in the format conversationName,oxygen+50
    // TODO in the future we can simplify this a lot by adding another field to the NPC conversation to specify oxygen refill
    void StoreItem(string itemName)
    {
        if (string.IsNullOrWhiteSpace(itemName)) return;

        string item = null, o2 = null;
        if (itemName.Contains(','))
        {
            var split = itemName.Split(',');
            item = split[0];
            o2 = split[1];
        }
        else if (itemName.Contains("oxygen+"))
        {
            o2 = itemName;
        }
        else
        {
            item = itemName;
        }


        if (o2 != null)
        {
            var split = o2.Split('+');
            if (split[0] == "oxygen")
            {
                int refill = Convert.ToInt32(split[1]);
                oxygen.RefillOxygen(refill);
                MainUIController.ConversationManager.SetConversationText(null, $"<i>CONSEGUISTE UN FILTRO AL {refill}%</i>", false);
            }
        }

        if (item != null)
        {
            PlayerData.itemsNamesInventory.Add(item);
            MainUIController.UIInventoryManager.AddIventoryItem(item);
        }
    }

    public void Seduce()
    {
        talkableNPC.Seduce();
    }

    public void Threaten()
    {
        talkableNPC.Threaten();
    }

    public void Steal()
    {
        var result = talkableNPC.Steal();

        if (result == 100)
        {
            TalkToNPC(talkableNPC);
        }

        else if (result > 0)
        {
            oxygen.RefillOxygen(result);
            Debug.Log("You stole " + result + " oxygen from the NPC!");
        }
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
            if (talkableNPC)
            {
                StoreItem(talkableNPC.Talk());
            }
            else
            {
                isTalking = false;
            }
            AudioController.PlaySFX(interactClip);
        }
        else if (talkableNPC)
        {
            AudioController.PlaySFX(interactClip);
            TalkToNPC(talkableNPC);
        }
        else if (canVeredict)
        {
            SceneManager.LoadScene("EndGameVeredict");
        }
        else if (MainUIController.ConversationManager.IsConversationActive())
        {
            MainUIController.ConversationManager.EndConversation();
        }
    }
}
