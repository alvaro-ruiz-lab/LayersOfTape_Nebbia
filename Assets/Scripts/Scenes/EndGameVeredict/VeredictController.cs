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



    private IEnumerator LoadHall()
    {
        yield return new WaitForSeconds(0.5f);

        Debug.Log("Loading Hall Scene...");

        Player.Instance.gameObject.SetActive(true);
        SceneManager.LoadScene("Hall");
    }

    

    public void CallVeredict()
    {
        StartCoroutine(LoadHall());
        return;

        foreach (Suspect suspect in suspects)
        {
            bool found = UniversalGameController.ItemData.TryGetByName(suspect.clue?.clueName, out var itemData);
            if (!found || itemData.associatedNPC != suspect.suspectName)
            {
                LoadDefeat();
            }
        }
        LoadVictory();
    }



    private void LoadDefeat()
    {
        losePanel.SetActive(true);
    }

    private void LoadVictory()
    {
        winPanel.SetActive(true);
    }
}
