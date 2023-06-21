using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int health = 1;
    [SerializeField] float hitTimer = 1;
    [SerializeField] float baseFiringCD = 0.35f;
    [SerializeField] float weaponSwapCD = 0.1f;
    [SerializeField] GameObject[] weapons;

    [Header("Projectile")]
    [SerializeField] GameObject projectile;

    [Header("Game Over")]
    [SerializeField] GameObject gameOver;

    int weaponChoice;
    float fireRateCounter;
    bool hasDied;
    float hitCounter;
    List<Weapon> weaponClassList;
    Vector3 mousePos;

    // Start is called before the first frame update
    void Start()
    {
        weaponChoice = 0;
        fireRateCounter = 0;
        hasDied = false;
        hitCounter = 0;
        
        // Hide all weapons before showing just the first
        foreach (GameObject w in weapons)
        {
            weaponClassList.Add(w.GetComponent<Weapon>());
            w.SetActive(false);
        }
        SwitchWeapon(weaponChoice);
    }

    // Update is called once per frame
    void Update()
    {
        // Mouse movement
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.rotation = Quaternion.LookRotation(Vector3.forward, mousePos - transform.position);

        // Mouse inputs
        if (Input.GetMouseButton(0) && fireRateCounter >= weaponClassList[weaponChoice].firingMultiplier * baseFiringCD)
        {
            fireRateCounter = 0;
            FireWeapon();
        }
        if (Input.GetMouseButtonDown(1) && fireRateCounter >= weaponSwapCD)
        {
            fireRateCounter = 0;
            SwitchWeapon(weaponChoice + 1);
        }

            fireRateCounter += Time.deltaTime;
            hitCounter += Time.deltaTime;
    }

    // Change weapons to next one in weaponClassList 
    // Input: Index of new weapon in list
    void SwitchWeapon(int newWeapon)
    {
        weapons[weaponChoice].SetActive(false);
        weaponChoice = newWeapon % weapons.Length;
        weapons[weaponChoice].SetActive(true);
    }

    // Fire weapon, with differing behaviour if the weapon is multishot
    void FireWeapon()
    {
        if (weaponClassList[weaponChoice].isMultishot == true)
        {
            float projDivision = weaponClassList[weaponChoice].shotArc / (weaponClassList[weaponChoice].projectileCount - 1);
            float currAngle = transform.rotation.eulerAngles.z - (weaponClassList[weaponChoice].shotArc / 2);
            for (int i = 0; i < weaponClassList[weaponChoice].projectileCount; i++)
            {
                CreateProjectile(currAngle);
                currAngle += projDivision;
            }
        }
        else
        {
            CreateProjectile(transform.rotation.eulerAngles.z);
        }
    }

    // Create projectile and apply settings dependnat on weapon's stats
    // Input: z-rotation to spawn the projectile
    void CreateProjectile(float spawnAngle)
    {
        Quaternion spawnRotation = Quaternion.Euler(0, 0, spawnAngle);
        GameObject newProjectile = Instantiate(projectile, transform.position, spawnRotation) as GameObject;
        newProjectile.GetComponent<Projectile>().ApplyProjectileSettings(weaponClassList[weaponChoice]);
    }

    // Reduce health by 1 and reset hit timer. If health at 0, activate game over object
    public void ReduceHealth()
    {
        if (hitCounter >= hitTimer)
        {
            health--;
            if (health <= 0 && hasDied == false)
            {
                hasDied = true;
                gameOver.SetActive(true);
            }
        }
        
    }
}
