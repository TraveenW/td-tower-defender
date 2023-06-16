using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOver : MonoBehaviour
{
    [SerializeField] float activateElementDelay = 1;
    [SerializeField] GameObject gameOverTransition;
    [SerializeField] GameObject sceneTransition;

    [Header("Time Counter")]
    [SerializeField] GameObject timeInput;
    [SerializeField] GameObject timeOutput;

    [Header("Hit Counter")]
    [SerializeField] GameObject hitInput;
    [SerializeField] GameObject hitOutput;

    [Header("Other UI Elements")]
    [SerializeField] GameObject gameOverText;
    [SerializeField] GameObject restartPrompt;

    GameObject[] textUIChildren;
    List<TextMeshProUGUI> textUI;
    bool promptClick = false;

    // Start is called before the first frame update
    void Start()
    {
        promptClick = false;
        textUI = new List<TextMeshProUGUI>();

        textUIChildren = new GameObject[] { gameOverText, timeOutput, hitOutput, restartPrompt };
        foreach (GameObject u in textUIChildren)
        {
            textUI.Add(u.GetComponent<TextMeshProUGUI>());
        }

        StartCoroutine(ExecuteGameOver());
    }

    IEnumerator ExecuteGameOver()
    {
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(activateElementDelay);
        sceneTransition.SetActive(true);
        gameOverTransition.GetComponent<UIImageFade>().FadeImage(1);
        timeOutput.GetComponent<TwoLineStatDisplay>().UpdateDisplay(timeInput.GetComponent<TimerSystem>().shortElapsedTime, ": ");
        hitOutput.GetComponent<TwoLineStatDisplay>().UpdateDisplay(hitInput.GetComponent<HitSystem>().hitNumber, ": ");

        foreach (TextMeshProUGUI line in textUI)
        {
            yield return new WaitForSecondsRealtime(activateElementDelay);
            line.color = new Color(line.color.r, line.color.g, line.color.b, 1);
        }
        promptClick = true;
    }

    private void Update()
    {
        if ((Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1)) && promptClick == true)
        {
            promptClick = false;
            sceneTransition.GetComponent<SceneTransition>().ReloadScene();
        }
    }
}
