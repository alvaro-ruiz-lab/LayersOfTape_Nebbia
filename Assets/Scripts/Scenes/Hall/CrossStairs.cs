using UnityEngine;

public class CrossStairs : MonoBehaviour
{
    [SerializeField] private BoxCollider2D myCollider;



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            UpdatePlayerLayer(collision);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            UpdatePlayerLayer(collision);
        }
    }



    private void UpdatePlayerLayer(Collider2D collision)
    {
        // Sacar pos de player y trigger
        float thisColliderCenterY = myCollider.bounds.center.y;
        float playerCenterY = collision.bounds.center.y;

        bool isDowning = playerCenterY > thisColliderCenterY;

        int newLayer = isDowning ? PlayerData.layerOnPB : PlayerData.layerOnP1;

        if (Player.Instance.GetComponent<SpriteRenderer>().sortingOrder != newLayer)
        {
            print($"Order layer de player {Player.Instance.GetComponent<SpriteRenderer>().sortingOrder}");
            Player.Instance.GetComponent<SpriteRenderer>().sortingOrder = newLayer;
            print($"NEW Order layer de player {Player.Instance.GetComponent<SpriteRenderer>().sortingOrder}");
        }
    }
}