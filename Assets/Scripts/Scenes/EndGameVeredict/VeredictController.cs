using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VeredictController : MonoBehaviour
{
    [SerializeField] private List<Suspect> suspects = new();
    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject losePanel;



    private void Awake()
    {
        Player.Instance.gameObject.SetActive(false);
        winPanel.SetActive(false);
        losePanel.SetActive(false);
    }



    public void CallVeredict()
    {
        foreach (Suspect suspect in suspects)
        {
            bool found = UniversalGameController.ItemData.TryGetByName(suspect.clue?.clueName, out var itemData);
            if (!found || itemData.associatedNPC != suspect.suspectName || !itemData.good)
            {
                ShowDefeat();
                return;
            }
        }
        ShowVictory();
    }

    private void ShowDefeat()
    {
        losePanel.SetActive(true);
    }

    private void ShowVictory()
    {
        winPanel.SetActive(true);
    }

    public void RestartGame()
    {
        Player.Instance.gameObject.SetActive(true);
        MainUIController.Instance.RestartGame();
    }

    public void BackToHall()
    {
        Player.Instance.gameObject.SetActive(true);
        SceneManager.LoadScene("Hall");
    }
}
