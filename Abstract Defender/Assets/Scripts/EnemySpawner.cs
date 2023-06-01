using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] float spawnMultiplier = 1f;
    [SerializeField] float spawnRadius = 18;

    int waveNumber;
    int waveStartTime;
    float spawnCooldown;
    float[] currentWave;
    TimerSystem gameTimer;

    [SerializeField] GameObject enemyTriangle;
    [SerializeField] GameObject enemySquare;
    [SerializeField] GameObject enemyHexagon;

    // Updates currentWave to the new wave
    void UpdateCurrentWave(float[,] newWave, int waveNum)
    {
        int rowLength = newWave.GetLength(1);
        for (int i = 0; i < rowLength; i++)
        {
            currentWave[i] = newWave[waveNum, i];
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        spawnCooldown = 0;
        waveStartTime = 0;
        waveNumber = 1;
        currentWave = new float[WaveArray.waveArray.GetLength(1)];
        UpdateCurrentWave(WaveArray.waveArray, 0);
        gameTimer = GameObject.FindObjectOfType<TimerSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        // Updates wave after specified time is reached
        if (gameTimer.shortElapsedTime >= currentWave[0])
        {
            waveStartTime = (int) currentWave[0];
            UpdateCurrentWave(WaveArray.waveArray, waveNumber);
            waveNumber++;
        }

        // Spawn enemy when spawnCooldown is over
        if (spawnCooldown >= currentWave[1] * spawnMultiplier)
        {
            SpawnEnemy();
            spawnCooldown = 0;
        }
    }

    // Create enemy at a random position out of view, with random allowed attributes
    void SpawnEnemy()
    {
        float spawnAngle = Random.Range(0, 360);
        int randomEnemy = 2;
        bool isEnemyChosen = false;
        int enemyHealth;
        int enemyTypeID;
        float[] enemyCoOrds;
        GameObject enemyType = enemyTriangle;

        // Determine enemy attributes
        while (!isEnemyChosen)
        {
            randomEnemy = Random.Range(2, currentWave.Length);
            if (currentWave[randomEnemy] != 0)
            {
                isEnemyChosen = true;
            }
        }
        randomEnemy -= 2;
        enemyHealth = 1 + (randomEnemy % 4);
        enemyTypeID = 1 + Mathf.FloorToInt(randomEnemy / 4);
        enemyCoOrds = CartesianAndPolar.ConvertToCartesian(spawnRadius, spawnAngle);

        // Spawn enemy and set attributes
        switch (enemyTypeID)
        {
            case 1:
                break;
            case 2:
                enemyType = enemySquare;
                break;
            case 3:
                enemyType = enemyHexagon;
                break;
            default:
                Debug.Log("ERROR: Unable to determine correct enemy type, reverted to Triangle.\nenemyType value: " + enemyTypeID.ToString());
                break;
        }

        GameObject newEnemy = Instantiate(enemyType, new Vector3(enemyCoOrds[0], enemyCoOrds[1], 0), Quaternion.identity) as GameObject;
        newEnemy.transform.Rotate(0, 0, spawnAngle + 180f, Space.World);
        // IMPORTANT: Set enemy health when enemy class is finished
    }
}
