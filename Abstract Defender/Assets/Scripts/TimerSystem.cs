using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimerSystem : MonoBehaviour
{
    public int shortElapsedTime;
    float elapsedTime;
    
    // Start is called before the first frame update
    void Start()
    {
        elapsedTime = 0;
        shortElapsedTime = 0;
        gameObject.GetComponent<TwoLineStatDisplay>().UpdateDisplay(0);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        elapsedTime += Time.fixedDeltaTime;
        if (Mathf.FloorToInt(elapsedTime) > shortElapsedTime)
        {
            shortElapsedTime++;
            gameObject.GetComponent<TwoLineStatDisplay>().UpdateDisplay(shortElapsedTime);
        }
    }
}
