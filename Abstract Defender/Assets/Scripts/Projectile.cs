using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    float speed = 24;
    
    int pierce = 1;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(0, speed * Time.deltaTime, 0), Space.Self);
    }

    // Apply projectile settings using the settings from a weapon
    // Input: The weapon object to get settings from
    public void ApplyProjectileSettings(Weapon wepSettings)
    {
        pierce = wepSettings.pierce;
        if (pierce > 1)
        {
            gameObject.tag = "Piercing Proj";
        }
        gameObject.GetComponent<DespawnOutOfRange>().despawnRange = wepSettings.shotDistance;
    }
}
