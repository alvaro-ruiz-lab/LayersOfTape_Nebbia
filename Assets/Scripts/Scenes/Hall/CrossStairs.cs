using UnityEngine;

public class CrossStairs : MonoBehaviour
{
    private BoxCollider2D myTrigger;
    private ColliderHeightHandler handler;



    private void Awake()
    {
        myTrigger = GetComponent<BoxCollider2D>();
        handler = GetComponentInParent<ColliderHeightHandler>();
    }



    private void OnTriggerExit2D(Collider2D other)
    {
        handler.HandlePlayerExitStairs(myTrigger, other);
    }
}
