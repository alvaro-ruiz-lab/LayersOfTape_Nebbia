using UnityEngine;

public class ColliderHeightHandler : MonoBehaviour
{
    [SerializeField] private Collider2D[] PBColliders;
    [SerializeField] private Collider2D[] P1Colliders;

    [SerializeField] private bool startOnP1;

    private SpriteRenderer playerSprite;



    private void Start()
    {
        playerSprite = Player.Instance.GetComponent<SpriteRenderer>();
        SetFloor(isOnP1: startOnP1);
    }



    public void HandlePlayerExitStairs(Collider2D stairsTrigger, Collider2D player)
    {
        if (!player || !stairsTrigger || !player.CompareTag("Player")) return;

        // "Exit from above" = player's bottom ended above trigger's top
        bool exitedFromAbove = player.bounds.min.y >= stairsTrigger.bounds.max.y;

        SetFloor(isOnP1: exitedFromAbove);
    }

    private void SetFloor(bool isOnP1)
    {
        SetEnabled(P1Colliders, isOnP1);
        SetEnabled(PBColliders, !isOnP1);
        playerSprite.sortingOrder = isOnP1 ? PlayerData.layerOnP1 : PlayerData.layerOnPB;
    }

    private static void SetEnabled(Collider2D[] colliders, bool enabled)
    {
        foreach (var c in colliders)
        {
            c.enabled = enabled;
        }
    }
}
