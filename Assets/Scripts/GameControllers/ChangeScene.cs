using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    [SerializeField] private string targetSceneName;

    // Control global de si se puede cambiar de escena
    public static bool CanChangeScene = false;
    private SpriteRenderer playerSprite;

    private void Start()
    {
        playerSprite = Player.Instance.GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!CanChangeScene) return; // Si no está permitido, salir

        print("Trigger entered by: " + other.name);
        if (other.CompareTag("Player"))
        {
            if (targetSceneName == "SecretChamber")
            {
                bool isOnCorrectLayer = playerSprite.sortingOrder <= PlayerData.layerOnPB;
                if (!isOnCorrectLayer) return;
            }

            if (targetSceneName == "ToxicRoom" || targetSceneName == "PrivateRoom")
            {
                bool isOnCorrectLayer = playerSprite.sortingOrder >= PlayerData.layerOnP1;
                if (!isOnCorrectLayer) return;
            }

            PlayerData.lastScene = targetSceneName;
            SceneManager.LoadScene(targetSceneName);
        }
    }
}
