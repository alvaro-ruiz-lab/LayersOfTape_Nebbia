using UnityEngine;

public class CameraTracker : MonoBehaviour
{

    [SerializeField] private Transform target;
    [SerializeField] private float cameraSpeed;
    private float t;



    private void Start()
    {
        if (target == null)
        {
            FindAnyObjectByType<Player>();
        }
    }



    private void Update()
    {
        if (target == null) return;

        t = cameraSpeed * Time.deltaTime;
        Mathf.Clamp01(t);

        Vector3 newPos = new Vector3(target.position.x, target.position.y, -10f);
        transform.position = Vector3.Lerp (transform.position, newPos, t);
        transform.rotation = Quaternion.Lerp (transform.rotation, target.rotation, t);
    }
}
