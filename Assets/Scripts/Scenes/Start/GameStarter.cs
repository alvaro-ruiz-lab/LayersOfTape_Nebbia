using AlvaroRuiz.Projects.GameControll.Audio;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStarter : MonoBehaviour
{
    void Start()
    {
        StopAllCoroutines();
        StartCoroutine(LoadGame());
    }

    private IEnumerator LoadGame()
    {
        AudioController.PlayInitMusic();
        yield return new WaitForSeconds(2.5f);

        Player.Oxygen.RefillOxygen(100f);

        SceneManager.LoadScene("OutDoor");
    }
}
