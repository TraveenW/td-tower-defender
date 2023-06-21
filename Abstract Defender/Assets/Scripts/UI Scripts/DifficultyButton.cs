using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultyButton : MonoBehaviour
{
    [SerializeField] float difficultyMultiplier = 1;
    [SerializeField] GameObject EnemyController;
    [SerializeField] GameObject GameDisplay;

    // Turn on Enemy Controller and Game Display elements; and disable Main Menu
    public void StartGame()
    {
        EnemyController.SetActive(true);
        EnemyController.GetComponent<EnemySpawner>().spawnRateMultiplier *= difficultyMultiplier;
        EnemyController.GetComponent<EnemySpawner>().speedMultiplier *= difficultyMultiplier;
        GameDisplay.SetActive(true);

        transform.parent.gameObject.SetActive(false);
    }
}
