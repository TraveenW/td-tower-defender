using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float baseFiringCD = 0.35f;
    [SerializeField] float weaponSwapCD = 0.1f;

    [Header("Weapons")]
    [SerializeField] GameObject shortbow;
    [SerializeField] GameObject crossbow;
    [SerializeField] GameObject launcher;

    [Header("Projectile")]
    [SerializeField] GameObject projectile;

    int weaponChoice;
    float fireRateCounter;
    GameObject[] weaponObjectList;
    Weapon[] weaponClassList;
    Vector3 mousePos;

    // Start is called before the first frame update
    void Start()
    {
        weaponChoice = 0;
        fireRateCounter = 0;
        weaponObjectList = new GameObject[] { shortbow, crossbow, launcher };
        weaponClassList = new Weapon[] { shortbow.GetComponent<Weapon>(), crossbow.GetComponent<Weapon>(), launcher.GetComponent<Weapon>() }; 
        
        // Hide all weapons before showing just the first
        foreach (GameObject g in weaponObjectList)
        {
            g.SetActive(false);
        }
        SwitchWeapon(weaponChoice);
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.rotation = Quaternion.LookRotation(Vector3.forward, mousePos - transform.position);
        if (fireRateCounter < weaponClassList[weaponChoice].firingMultiplier * baseFiringCD)
        {
            fireRateCounter += Time.deltaTime;
        }


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
    }

    // Change weapons to next one in weaponClassList 
    // Input: Index of new weapon in weaponClassList
    void SwitchWeapon(int newWeapon)
    {
        weaponObjectList[weaponChoice].SetActive(false);
        weaponChoice = newWeapon % weaponObjectList.Length;
        weaponObjectList[weaponChoice].SetActive(true);
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
}
