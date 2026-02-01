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
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // TODO quizás manejar race condition  && Player.PI.currentActionMap.name == "Player" pero tener en cuenta que el exit se tiene que ejecutar después
        if (other.TryGetComponent(out NPC npc) && npc == playerScript.talkableNPC)
        {
            playerScript.talkableNPC = null;
        }
    }
}
