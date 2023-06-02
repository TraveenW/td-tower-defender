using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject enemyTriangle;
    [SerializeField] GameObject enemySquare;

    float baseSpeed = 2; // IMPORTANT: When GUI is finished, make baseSpeed public
    float typeSpeedReduction = 0.2f;
    
    float pierceCooldown = 0.7f;
    float kbDuration = 0.5f;
    float splitArcHalf = 30f;

    int enemyID = 1;
    int health = 1;
    float finalSpeed = 2;

    float currPierceCooldown = 0;

    // When creating enemies, use this function to set their speed and health
    public void CreateEnemySettings(int newID, int newHealth)
    {
        enemyID = newID;
        finalSpeed = baseSpeed - typeSpeedReduction * (newID - 1);
        UpdateSkin(newHealth);
    }

    // Changes the sprite color according to health
    void UpdateSkin(int nextHealth)
    {
        SpriteRenderer sr = gameObject.GetComponent<SpriteRenderer>();

        health = nextHealth;
        switch (health)
        {
            case 0:
                sr.color = Color.clear;
                break;
            case 1:
                sr.color = Color.red;
                break;
            case 2:
                sr.color = Color.green;
                break;
            case 3:
                sr.color = Color.blue;
                break;
            case 4:
                sr.color = Color.black;
                break;
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        gameObject.transform.Translate(new Vector3(0, finalSpeed * Time.deltaTime, 0));
        if (currPierceCooldown < pierceCooldown)
        {
            currPierceCooldown += Time.deltaTime;
        }
    }

    // IMPORTANT: Build rest of script when player and projectile scripts are built
}
