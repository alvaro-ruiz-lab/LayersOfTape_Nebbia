using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Clue : MonoBehaviour
{
    [SerializeField] public Image clueImage;
    [SerializeField] public TMP_Text clueText;

    [NonSerialized] public string clueName;
}
