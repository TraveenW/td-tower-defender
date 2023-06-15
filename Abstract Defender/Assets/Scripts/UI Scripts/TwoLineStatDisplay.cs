using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TwoLineStatDisplay : MonoBehaviour
{
    [SerializeField] string statName;
    TMP_Text textDisplay;

    // Update the Stat Display text with a whole number
    public void UpdateDisplay(int wholeNumber, string separator = ":\n")
    {
        textDisplay.text = statName + separator + wholeNumber.ToString();
    }

    private void Start()
    {
        textDisplay = gameObject.GetComponent<TMP_Text>();
        UpdateDisplay(0);
    }
}
