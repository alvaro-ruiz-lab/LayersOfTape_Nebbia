using UnityEngine;

public class HallManager : MonoBehaviour
{
    void Start()
    {
        Player.Instance.transform.position = new Vector3(0, -3.5f, 0);
    }
}
