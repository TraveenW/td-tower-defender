using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonShapeHitbox : MonoBehaviour
{
    [SerializeField] float alphaHitThreshold = 0.9f;
    
    // Set button hitbox to be the shape of the image
    private void Start()
    {
        gameObject.GetComponent<Image>().alphaHitTestMinimumThreshold = alphaHitThreshold;
    }
}
