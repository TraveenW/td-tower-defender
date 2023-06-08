using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitSystem : MonoBehaviour
{
    [HideInInspector] public int hitNumber;
    
    // Start is called before the first frame update
    void Start()
    {
        hitNumber = 0;
    }

    // Increment hit and update display
    public void IncrementHit()
    {
        hitNumber++;
        gameObject.GetComponent<TwoLineStatDisplay>().UpdateDisplay(hitNumber);
    }
}
