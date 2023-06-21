using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DespawnOutOfRange : MonoBehaviour
{
    public float despawnRange = 40;
    float[] distanceFromCenter;

    // Destroy object if they are a specific distance from the center
    void Update()
    {
        distanceFromCenter = CartesianAndPolar.ConvertToPolar(gameObject.transform.position.x, gameObject.transform.position.y);
        if (distanceFromCenter[0] >= despawnRange && despawnRange > 0)
        {
            Destroy(gameObject);
        }
    }
}
