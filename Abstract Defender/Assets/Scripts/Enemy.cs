using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] int enemyID;
    [SerializeField] float speed;

    [Header("Knockback")]
    [SerializeField] float kbDuration;
    [SerializeField] float kbMultiplier;

    [Header("Splitting")]
    [SerializeField] float splitArcHalf;
    [SerializeField] int splitEnemyCount;
    [SerializeField] GameObject enemySplitup;
    
    int health = 1;
    float pierceCooldown = 1;

    float pierceCounter = 0;
    float hitCounter = 0;
    float speedStored;

    // When creating enemies, use this function to set their speed and health
    // Input newHealth: The enemy's health
    public void CreateEnemySettings(int newHealth)
    {
        speed *= GameObject.Find("Enemy Controller").GetComponent<EnemySpawner>().speedMultiplier;
        speedStored = speed;
        UpdateHealth(newHealth);
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.Translate(new Vector3(0, speed * Time.deltaTime, 0));
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
    // Input: The new health to change to
    void UpdateHealth(int newHealth)
    {
        SpriteRenderer sr = gameObject.GetComponent<SpriteRenderer>();

        health = newHealth;
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

        for (int n = 0; n < splitEnemyCount; n++)
        {
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

            // Create new enemy and apply attributes
            GameObject newEnemy = Instantiate(enemySplitup, transform.position, transform.rotation) as GameObject;
            newEnemy.transform.Rotate(0, 0, randomAngle);
            newEnemy.GetComponent<Enemy>().CreateEnemySettings(health - 1);
            newEnemy.GetComponent<Enemy>().SplitEnemyKnockback(kbDuration);
        }
        StartCoroutine(KillEnemy());
    }

    // Starts Knockback coroutine. Only here so that coroutine can be executed on new enemy
    public void SplitEnemyKnockback(float splitKBDuration)
    {
        StartCoroutine(Knockback(splitKBDuration, kbMultiplier));
    }


    // Send enemy backwards for kbDuration seconds, and then reorient towards the center
    // Input duration: How long knockback will go for
    // Input speedMultiplier: Multiply its regular speed when knocked back
    IEnumerator Knockback(float duration, float speedMultiplier)
    {
        float[] newPolarCoOrds;

        // Reverse finalSpeed and multiply its magnitude
        speed -= speedMultiplier * speedStored;
        yield return new WaitForSeconds(duration);

        // Reorient enemy and reset finalSpeed
        newPolarCoOrds = CartesianAndPolar.ConvertToPolar(transform.position.x, transform.position.y);
        transform.eulerAngles = new Vector3(0, 0, newPolarCoOrds[1] + 90f);
        speed = speedStored;
        yield return null;
    }

    // Play death particles before finally destroying enemy object
    IEnumerator KillEnemy()
    {
        ParticleSystem deathParticle = transform.GetChild(0).GetComponent<ParticleSystem>();
        var particleSettings = deathParticle.main;

        speed = 0;
        gameObject.GetComponent<Collider2D>().enabled = false;
        particleSettings.startColor = gameObject.GetComponent<SpriteRenderer>().color;
        UpdateHealth(0);
        deathParticle.Play();
        yield return new WaitForSeconds(deathParticle.main.duration);
        Destroy(gameObject);
        yield return null;
    }
}
