using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteFade : MonoBehaviour
{
    [SerializeField] float fadeDuration = 1.3f;
    [SerializeField] float targetAlphaMaxDefault = 1;
    [SerializeField] float targetAlphaMinDefault = 0;

    // Called by other scripts to fade in or out an image/sprite 
    public void FadeIn()
    {

    }
    public void FadeIn(float targetAlpha)
    {

    }

    public void FadeOut()
    {

    }
    public void FadeOut(float targetAlpha)
    {

    }

    IEnumerator FadeSprite()
    {
        yield return null;
    }
}
