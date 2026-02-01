using UnityEngine;
using UnityEngine.Tilemaps;

public class CheckLayer : MonoBehaviour
{
    [SerializeField] private TilemapCollider2D myCollider;



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            CheckPlayerSortingLayer(collision.gameObject);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            CheckPlayerSortingLayer(collision.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Vector3 playerLastPos = Player.Instance.transform.position;

            float thisColliderCenterY = myCollider.bounds.center.y;
            float playerCenterY = collision.bounds.center.y;

            // Si el player viene desde abajo y esta en sorting layer de PB, resetear
            if (playerCenterY < thisColliderCenterY)
            {
                if (Player.Instance.GetComponent<SpriteRenderer>().sortingOrder <= PlayerData.layerOnPB)
                {
                    Debug.Log("Colision desde abajo");
                    collision.gameObject.transform.position = playerLastPos;
                }
            }
        }
    }

    private void CheckPlayerSortingLayer(GameObject player)
    {       
        // Si la sorting order es superior a PB, ignorar colision (atravesable)
        if (Player.Instance.GetComponent<SpriteRenderer>().sortingOrder > PlayerData.layerOnPB)
        {
            // Hacer el collider un trigger para que sea atravesable
            myCollider.isTrigger = true;
            Debug.Log($"Player sorting order {Player.Instance.GetComponent<SpriteRenderer>().sortingOrder} > {PlayerData.layerOnPB} - Collider atravesable");
        }
        else
        {
            // Hacer el collider sólido (no atravesable)
            myCollider.isTrigger = false;
            Debug.Log($"Player sorting order {Player.Instance.GetComponent<SpriteRenderer>().sortingOrder} <= {PlayerData.layerOnPB} - Collider tangible");
        }
    }
}
