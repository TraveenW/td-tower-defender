using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float kbDuration = 0.15f;
    [SerializeField] float splitArcHalf = 50f;
    [SerializeField] int splitEnemyCount = 2;
    [SerializeField] GameObject enemySplitup;

    float baseSpeed; 
    float typeSpeedReduction = 0.2f;
    
    float pierceCooldown = 1;
    
    int enemyID = 1;
    int health = 1;
    float finalSpeed = 2;
    float finalSpeedStorage;

    float pierceCounter = 0;

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.Translate(new Vector3(0, finalSpeed * Time.deltaTime, 0));
        if (pierceCounter < pierceCooldown)
        {
            pierceCounter += Time.deltaTime;
        }
    }

    // Take damage whenever colliding with projectile
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.tag == "Projectile")
        {
            other.transform.GetComponent<Projectile>().DecreasePierce();
            TakeDamage();
        }
        else if (other.transform.tag == "Piercing Proj" && pierceCounter >= pierceCooldown)
        {
            pierceCounter = 0;
            other.transform.GetComponent<Projectile>().DecreasePierce();
            TakeDamage();
        }
    }

    // When creating enemies, use this function to set their speed and health
    public void CreateEnemySettings(int newID, int newHealth)
    {
        enemyID = newID;
        baseSpeed = GameObject.Find("Enemy Controller").GetComponent<EnemySpawner>().speedMultiplier * 2;
        finalSpeed = baseSpeed - typeSpeedReduction * (newID - 1);
        finalSpeedStorage = finalSpeed;
        UpdateHealth(newHealth);
    }

    // Changes the sprite color according to health
    void UpdateHealth(int nextHealth)
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
    
    

    // Depending on enemy type, either damage enemy normally or split it. If at 1 health, will instead kill enemy
    void TakeDamage()
    {
        if (enemyID > 1 && health > 1)
        {
            SplitEnemy();
        }
        else if (enemyID == 1 && health > 1)
        {
            UpdateHealth(health - 1);
            StartCoroutine(Knockback());
        }
        else if (health <= 1)
        {
            StartCoroutine(KillEnemy());
        }
    }

    // Create two enemies and send them back at random angles. Then kill current enemy.
    void SplitEnemy()
    {
        float randomAngle = 0;
        int splitArcSide = 1;
        GameObject newSplitEnemy;

        for (int n = 0; n < splitEnemyCount; n++)
        {
            newSplitEnemy = Instantiate(enemySplitup, transform.position, transform.rotation) as GameObject;
            switch (splitArcSide)
            {
                case 1:
                    randomAngle = Random.Range(-90, -splitArcHalf);
                    break;
                case -1:
                    randomAngle = Random.Range(splitArcHalf, 90);
                    break;
            }
            splitArcSide *= -1;
            newSplitEnemy.transform.Rotate(0, 0, randomAngle);
            newSplitEnemy.GetComponent<Enemy>().CreateEnemySettings(enemyID - 1, health - 1);
            newSplitEnemy.GetComponent<Enemy>().SplitEnemyKnockback();
        }
        StartCoroutine(KillEnemy());
    }

    // Starts Knockback coroutine
    public void SplitEnemyKnockback()
    {
        StartCoroutine(Knockback());
    }

    // Send enemy backwards for kbDuration seconds, find player at center, and then reorient
    IEnumerator Knockback()
    {
        float[] newPolarCoOrds;
        float kbSpeedMultiplier = 1 + 1 / (kbDuration / 2);

        finalSpeed -= finalSpeedStorage * kbSpeedMultiplier;
        for (float k = 0; k <= kbDuration; k += Time.deltaTime * kbSpeedMultiplier)
        {
            transform.Translate(new Vector3(0, finalSpeed * Time.deltaTime * kbSpeedMultiplier, 0));
            yield return new WaitForSeconds(Time.deltaTime);
        }
        newPolarCoOrds = CartesianAndPolar.ConvertToPolar(transform.position.x, transform.position.y);
        transform.eulerAngles = new Vector3(0, 0, newPolarCoOrds[1] + 90f);
        finalSpeed += finalSpeedStorage * kbSpeedMultiplier;
        yield return null;
    }

    // Play death particles before finally destroying enemy object
    IEnumerator KillEnemy()
    {
        ParticleSystem deathParticle = transform.GetChild(0).GetComponent<ParticleSystem>();
        var particleSettings = deathParticle.main;

        finalSpeed = 0;
        gameObject.GetComponent<Collider2D>().enabled = false;
        particleSettings.startColor = gameObject.GetComponent<SpriteRenderer>().color;
        UpdateHealth(0);
        deathParticle.Play();
        yield return new WaitForSeconds(deathParticle.main.duration);
        Destroy(gameObject);
        yield return null;
    }
}
