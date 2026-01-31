using UnityEngine;

public class CameraTracker : MonoBehaviour
{

    [SerializeField] private Transform target;
    [SerializeField] private float cameraSpeed;
    private float t;



    private void Awake()
    {
        if (target == null)
        {
            FindAnyObjectByType<Player>();
        }
    }



    private void Update()
    {
        t = cameraSpeed * Time.deltaTime;
        Mathf.Clamp01(t);
        transform.position = Vector2.Lerp (transform.position, target.position, t);
        transform.rotation = Quaternion.Lerp (transform.rotation, target.rotation, t);
    }
}
