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
        AudioController.PlayMusic("MainLoop");
        AudioController.PlayEnvSound(null, 1);
        AudioController.PlayEnvSound(null, 2);

        yield return new WaitForSeconds(2.5f);

        if (Player.Oxygen != null)
            Player.Oxygen.RefillOxygen(100f);

        SceneManager.LoadScene("OutDoor");
    }
}
