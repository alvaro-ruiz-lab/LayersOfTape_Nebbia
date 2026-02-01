using System;
using System.Collections;
using System.Collections.Generic;
using AlvaroRuiz.Projects.GameControll.Audio;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VeredictController : MonoBehaviour
{
    [SerializeField] private List<Suspect> suspects = new();


    private void Start()
    {
        Player.Instance.gameObject.SetActive(false);
    }

    private void OnEsc()
    {
        StartCoroutine(LoadHall());
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

    }

    private void LoadVictory()
    {

    }
}
