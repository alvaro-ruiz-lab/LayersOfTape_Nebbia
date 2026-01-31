using System.Collections;
using AlvaroRuiz.Projects.GameControll.Audio;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStarter : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(LoadGame());
    }

    private IEnumerator LoadGame()
    {
        AudioController.PlayInitMusic();
        yield return new WaitForSeconds(5f);

        SceneManager.LoadScene("Hall");
    }
}
