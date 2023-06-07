using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DespawnOutOfRange : MonoBehaviour
{
    float despawnRange = 35;
    float[] distanceFromCenter;

    // Check if object 
    void Update()
    {
        distanceFromCenter = CartesianAndPolar.ConvertToPolar(gameObject.transform.position.x, gameObject.transform.position.y);
        if (distanceFromCenter[0] >= despawnRange)
        {
            Destroy(gameObject);
        }
    }
}
