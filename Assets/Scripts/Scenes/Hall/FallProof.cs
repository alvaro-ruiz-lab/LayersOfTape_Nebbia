using UnityEngine;

public class FallProof : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            float playerLastPosY = Player.Instance.transform.position.y;

            bool isFalling = playerLastPosY > collision.gameObject.transform.position.y;

            // Comprobar si la colisión viene desde abajo
            if (isFalling)
            {
                Vector3 currentPlayerPos = Player.Instance.transform.position;
                Player.Instance.transform.position = new Vector3(currentPlayerPos.x, playerLastPosY, currentPlayerPos.z);
            }
        }
    }
}
