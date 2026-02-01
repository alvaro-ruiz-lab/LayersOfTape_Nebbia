using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    [SerializeField] private string targetSceneName;

    private int pbLayer;
    private int p1Layer;



    private void OnTriggerEnter2D (Collider2D other)
    {
        print("Trigger entered by: " + other.name);
        if (other.CompareTag("Player"))
        {
            if (targetSceneName == "SecretChamber")
            {
                bool isOnCorrectLayer = Player.Instance.gameObject.layer <= pbLayer;

                if (!isOnCorrectLayer)
                    return;
            }

            if (targetSceneName == "ToxicRoom" || targetSceneName == "PrivateRoom")
            {
                bool isOnCorrectLayer = Player.Instance.gameObject.layer <= p1Layer;

                if (!isOnCorrectLayer)
                    return;
            }

            SceneManager.LoadScene(targetSceneName);
        }
    }
}
