using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimerSystem : MonoBehaviour
{
    public TMP_Text timeDisplay;
    public int shortElapsedTime;
    float elapsedTime;

    // Update the Time Display text with the current time
    void UpdateDisplay(int currTime) 
    {
        timeDisplay.text = "Time:\n" + currTime.ToString();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        elapsedTime = 0;
        shortElapsedTime = 0;
        UpdateDisplay(0);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        elapsedTime += Time.fixedDeltaTime;
        if (Mathf.FloorToInt(elapsedTime) > shortElapsedTime)
        {
            shortElapsedTime++;
            UpdateDisplay(shortElapsedTime);
        }
    }
}
