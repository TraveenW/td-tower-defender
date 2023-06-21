using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] int enemyID = 0;
    [SerializeField] float kbDuration = 0.02f;
    [SerializeField] float kbMultiplier = 5;
    [SerializeField] float splitArcHalf = 50f;
    [SerializeField] int splitEnemyCount = 2;
    [SerializeField] GameObject enemySplitup;

    float baseSpeed; 
    float typeSpeedReduction = 0.2f;
    
    float pierceCooldown = 1;
    
    int health = 1;
    float finalSpeed = 2;
    float finalSpeedStorage;

    float pierceCounter = 0;
    float hitCounter = 0;

    // When creating enemies, use this function to set their speed and health
    // Input newID: The enemy's ID/type number
    // Input newHealth: The enemy's health
    public void CreateEnemySettings(int newHealth)
    {
        baseSpeed = GameObject.Find("Enemy Controller").GetComponent<EnemySpawner>().speedMultiplier * 2;
        finalSpeed = baseSpeed - typeSpeedReduction * enemyID;
        finalSpeedStorage = finalSpeed;
        UpdateHealth(newHealth);
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.Translate(new Vector3(0, finalSpeed * Time.deltaTime, 0));
        pierceCounter += Time.deltaTime;
        hitCounter += Time.deltaTime;
    }

    // Take damage whenever colliding with projectile. If piercing, also reset the pierce immunity.
    // If collision with player instead, rreduce player's health and kill enemy
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.tag == "Projectile" && hitCounter > kbDuration)
        {
            hitCounter = 0;
            other.transform.GetComponent<Projectile>().DecreasePierce();
            TakeDamage();
        }
        else if (other.transform.tag == "Piercing Proj" && pierceCounter >= pierceCooldown && hitCounter > kbDuration)
        {
            pierceCounter = 0;
            hitCounter = 0;
            other.transform.GetComponent<Projectile>().DecreasePierce();
            TakeDamage();
        }
        else if (other.transform.tag == "Player")
        {
            other.transform.GetComponent<Player>().ReduceHealth();
            KillEnemy();
        }
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

    // Depending on enemy type, either damage enemy normally or split it. If at 1 health or less, will instead kill enemy
    void TakeDamage()
    {
        if (enemyID > 0 && health > 1)
        {
            SplitEnemy();
        }
        else if (enemyID == 0 && health > 1)
        {
            UpdateHealth(health - 1);
            StartCoroutine(Knockback(kbDuration, kbMultiplier));
        }
        else if (health <= 1)
        {
            StartCoroutine(KillEnemy());
        }
    }

    // Create two enemies and send them back at specific random angles. Then kill current enemy.
    void SplitEnemy()
    {
        float randomAngle = 0;
        int splitArcSide = 1;
        GameObject newSplitEnemy;

        for (int n = 0; n < splitEnemyCount; n++)
        {
            newSplitEnemy = Instantiate(enemySplitup, transform.position, transform.rotation) as GameObject;

            // To exaggerate split, splitArcSide is used to remove the middle sector when randomising split
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

            // Apply attributes
            newSplitEnemy.GetComponent<Enemy>().CreateEnemySettings(health - 1);
            newSplitEnemy.GetComponent<Enemy>().SplitEnemyKnockback(kbDuration);
        }
        StartCoroutine(KillEnemy());
    }

    // Starts Knockback coroutine. Only here so that coroutine can be executed on new enemy
    public void SplitEnemyKnockback(float splitKBDuration)
    {
        StartCoroutine(Knockback(splitKBDuration, kbMultiplier));
    }


    // Send enemy backwards for kbDuration seconds, and then reorient towards the center
    IEnumerator Knockback(float duration, float speedMultiplier)
    {
        float[] newPolarCoOrds;

        // Reverse finalSpeed and multiply its magnitude
        finalSpeed -= speedMultiplier * finalSpeedStorage;
        yield return new WaitForSeconds(duration);

        // Reorient enemy and reset finalSpeed
        newPolarCoOrds = CartesianAndPolar.ConvertToPolar(transform.position.x, transform.position.y);
        transform.eulerAngles = new Vector3(0, 0, newPolarCoOrds[1] + 90f);
        finalSpeed = finalSpeedStorage;
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
