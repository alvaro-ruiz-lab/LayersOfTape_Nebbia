using UnityEngine;

public class HallManager : MonoBehaviour
{
    void Start()
    {
        switch (PlayerData.lastScene)
        {
            case "OutDoor":
                Player.Instance.transform.position = new Vector3(0, -3.5f, 0);
                break;

            case "PrivateRoom":
                Player.Instance.transform.position = new Vector3(-3.25f, 8.14f, 0);
                break;

            case "ToxicRoom":
                Player.Instance.transform.position = new Vector3(3.5f, 8.14f, 0);
                break;

            case "SecretChamber":
                Player.Instance.transform.position = new Vector3(0, 7f, 0);
                break;

            default:
                Player.Instance.transform.position = new Vector3(0, -3.5f, 0);
                break;
        }
    }
}
