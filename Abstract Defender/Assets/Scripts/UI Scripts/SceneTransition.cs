using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    bool initialIntro = true;
    UISpriteFade transitionFade;

    // Start is called before the first frame update
    void Start()
    {
        transitionFade = GetComponent<UISpriteFade>();  
        if (initialIntro)
        {
            initialIntro = false;
            transitionFade.FadeSprite(1, 0);
            gameObject.SetActive(false);
        }
    }

    // When called, fade to black and reload scene
    public void ReloadScene()
    {
        transitionFade.FadeSprite(1);
        StartCoroutine(StartReload());
    }

    IEnumerator StartReload()
    {
        for (float t = 0; t < 1; t += Time.unscaledDeltaTime / transitionFade.fadeDuration)
        {
            yield return null;
        }
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}
