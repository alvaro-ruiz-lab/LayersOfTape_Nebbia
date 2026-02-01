using System;
using UnityEngine;
using UnityEngine.UI;

public class Suspect : MonoBehaviour
{
    [SerializeField] string suspectName;
    [SerializeField] Image suspectImage;
    [NonSerialized] public Clue clue;


    void OnDrop(Clue droppedClue)
    {
        clue = droppedClue;
    }


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
