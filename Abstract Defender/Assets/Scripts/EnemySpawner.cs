using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    public float spawnRateMultiplier = 1f; 
    public float speedMultiplier = 1f;
    [SerializeField] float freeplayRampupMultiplier = 0.01f;
    [SerializeField] float spawnRadius = 18;
    [SerializeField] GameObject timerObject;

    [Header("Enemy Types")]
    [SerializeField] int enemyHealthTypes = 4;
    [SerializeField] GameObject[] enemyList;

    int waveNumber;
    int maxWaveNumber;
    float spawnCooldown;
    float[] currentWave;
    TimerSystem gameTimer;

    // Start is called before the first frame update
    void Start()
    {
        spawnCooldown = 0;
        waveNumber = 1;
        maxWaveNumber = WaveArray.waveArray.GetLength(0);
        currentWave = new float[WaveArray.waveArray.GetLength(1)];
        UpdateCurrentWave(WaveArray.waveArray, 0);
        gameTimer = timerObject.GetComponent<TimerSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        // Updates wave after specified time is reached
        if (gameTimer.shortElapsedTime >= currentWave[0] && waveNumber < maxWaveNumber)
        {
            UpdateCurrentWave(WaveArray.waveArray, waveNumber);
            waveNumber++;
        }

        // Ramps up spawn rate in the freeplay/endless wave
        if (waveNumber == maxWaveNumber)
        {
            spawnRateMultiplier += Time.deltaTime * freeplayRampupMultiplier;
        }

        // Spawn enemy when spawnCooldown is over
        if (spawnCooldown >= currentWave[1] * (1 / spawnRateMultiplier))
        {
            SpawnEnemy();
            spawnCooldown = 0;
        }

        spawnCooldown += Time.deltaTime;
    }

    // Updates currentWave to the new wave
    void UpdateCurrentWave(float[,] newWave, int waveNum)
    {
        int rowLength = newWave.GetLength(1);
        for (int i = 0; i < rowLength; i++)
        {
            currentWave[i] = newWave[waveNum, i];
        }
    }

    // Create enemy at a random position out of view, with random allowed attributes
    void SpawnEnemy()
    {
        float spawnAngle = Random.Range(0, 360);
        int randomEnemy = 0;
        bool isEnemyChosen = false;
        int enemyHealth;
        int enemyTypeID;
        float[] enemyCoOrds;

        // Randomly choose an enemy allowed to spawn
        while (!isEnemyChosen)
        {
            randomEnemy = Random.Range(0, currentWave.Length - 2);
            if (currentWave[randomEnemy + 2] != 0)
            {
                isEnemyChosen = true;
            }
        }

        // Set attributes
        enemyHealth = 1 + (randomEnemy % enemyHealthTypes);
        enemyTypeID = Mathf.FloorToInt(randomEnemy / enemyHealthTypes);
        enemyCoOrds = CartesianAndPolar.ConvertToCartesian(spawnRadius, spawnAngle);
 
        // Spawn enemy and apply attributes
        GameObject newEnemy = Instantiate(enemyList[enemyTypeID], new Vector3(enemyCoOrds[0], enemyCoOrds[1], 0), Quaternion.identity) as GameObject;
        newEnemy.transform.Rotate(0, 0, spawnAngle + 90f, Space.Self);
        newEnemy.GetComponent<Enemy>().CreateEnemySettings(enemyHealth);
    }
}
