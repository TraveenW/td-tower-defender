using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    [SerializeField] float activateElementDelay = 1;
    [SerializeField] GameObject sceneTransition;

    [Header("Time Counter")]
    [SerializeField] GameObject timeInput;
    [SerializeField] GameObject timeOutput;

    [Header("Hit Counter")]
    [SerializeField] GameObject hitInput;
    [SerializeField] GameObject hitOutput;

    List<Transform> UIChildren;
    bool promptClick = false;

    // Start is called before the first frame update
    void Start()
    {
        promptClick = false;
        timeOutput.GetComponent<TwoLineStatDisplay>().UpdateDisplay(timeInput.GetComponent<TimerSystem>().shortElapsedTime, ": ");
        hitOutput.GetComponent<TwoLineStatDisplay>().UpdateDisplay(timeInput.GetComponent<HitSystem>().hitNumber, ": ");

        Time.timeScale = 0;
        foreach (Transform child in gameObject.transform)
        {
            if (child.tag == "Fade")
            {
                child.GetComponent<UISpriteFade>().FadeSprite(0, 1);
            }
            else
            {
                UIChildren.Add(child);
                child.gameObject.SetActive(false);
            }
        }
        StartCoroutine(ExecuteGameOver());
    }

    IEnumerator ExecuteGameOver()
    {
        foreach (Transform child in UIChildren)
        {
            yield return new WaitForSecondsRealtime(activateElementDelay);
            child.gameObject.SetActive(true);
        }
        promptClick = true;
    }

    private void Update()
    {
        if ((Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1)) && promptClick == true)
        {
            promptClick = false;
            sceneTransition.SetActive(true);
            sceneTransition.GetComponent<SceneTransition>().ReloadScene();
        }
    }
}
