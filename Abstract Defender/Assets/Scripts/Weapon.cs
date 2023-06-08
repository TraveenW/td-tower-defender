using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float firingMultiplier;
    public int pierce;
    public float shotDistance;

    [Header("Multishot")]
    public bool isMultishot;
    public int projectileCount;
    public float shotArc;
}
