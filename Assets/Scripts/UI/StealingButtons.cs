using UnityEngine;

public class StealingButtons : MonoBehaviour
{
    public void Seduce()
    {
        Player.Instance.Seduce();
    }

    public void Threaten()
    {
        Player.Instance.Threaten();
    }

    public void Steal()
    {
        Player.Instance.Steal();
    }
}
