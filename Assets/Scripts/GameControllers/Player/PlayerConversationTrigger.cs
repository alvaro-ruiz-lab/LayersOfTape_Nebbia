using UnityEngine;

public class PlayerConversationTrigger : MonoBehaviour
{
    private Player playerScript;



    private void Start()
    {
        playerScript = GetComponentInParent<Player>();
    }



    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out NPC npc))
        {
            playerScript.talkableNPC = npc;
        }
        else if (other.gameObject.CompareTag("Finish"))
        {
            playerScript.canVeredict = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent(out NPC npc) && npc == playerScript.talkableNPC)
        {
            playerScript.talkableNPC = null;
        }
        else if (other.gameObject.CompareTag("Finish"))
        {
            playerScript.canVeredict = false;
        }
    }
}
