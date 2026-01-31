using UnityEngine;

public class UniversalGameController : MonoBehaviour
{
    // Propiedades
    public static UniversalGameController Instance { get; private set; }



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
