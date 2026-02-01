using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    [SerializeField] private string targetSceneName;



    private void OnTriggerEnter2D (Collider2D other)
    {
        print("Trigger entered by: " + other.name);
        if (other.CompareTag("Player"))
        {

            if (targetSceneName == "SecretChamber")
            {
                bool isOnCorrectLayer = Player.Instance.gameObject.layer <= PlayerData.layerOnPB;

                if (!isOnCorrectLayer)
                    return;
            }

            if (targetSceneName == "ToxicRoom" || targetSceneName == "PrivateRoom")
            {
                bool isOnCorrectLayer = Player.Instance.gameObject.layer <= PlayerData.layerOnP1;

                if (!isOnCorrectLayer)
                    return;
            }

            PlayerData.lastScene = targetSceneName;
            SceneManager.LoadScene(targetSceneName);
        }
    }
}
