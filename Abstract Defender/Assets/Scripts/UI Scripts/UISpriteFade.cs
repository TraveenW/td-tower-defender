using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISpriteFade : MonoBehaviour
{
    public float fadeDuration = 1.3f;

    SpriteRenderer sprite;

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    // Called by other scripts to fade sprite to a new alpha value
    // Input: The goal alpha amount
    public void FadeSprite(float endAlpha)
    {
        float startAlpha = sprite.color.a;
        StartCoroutine(ChangeAlpha(startAlpha, endAlpha));
    }
    // Called by other scripts to fade sprite between two different alpha values
    // Input startAlpha: The starting alpha amount
    // Input endAlpha: The goal alpha amount
    public void FadeSprite(float startAlpha, float endAlpha)
    {
        StartCoroutine(ChangeAlpha(startAlpha, endAlpha));
    }
    
    IEnumerator ChangeAlpha(float start, float end)
    {
        for (float t = 0.0f; t < 1.0f; t += Time.unscaledDeltaTime / fadeDuration)
        {
            Color newColor = new Color(sprite.color.r, sprite.color.g, sprite.color.b, Mathf.Lerp(start, end, t));
            sprite.color = newColor;
            yield return null;
        }
    }
}
