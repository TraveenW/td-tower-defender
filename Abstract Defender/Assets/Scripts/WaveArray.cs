using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveArray : MonoBehaviour
{
    // Array rows represent each of the 20 waves, and the 21st endless wave
    // Columns are organised as:
    // Col1 - Time To Finish, Col2 - Spawn Timer, Col3+ - Allowed Enemies
    // Allowed Enemies columns are grouped in shapes, and each group is sorted by health:
    // T1, T2, T3, T4, S1, S2, S3, S4, H1, H2, H3, H4

    public static float[,] waveArray =
    { // TTF,   ST,     T1, T2, T3, T4, S1, S2, S3, S4, H1, H2, H3, H4
        { 10,   0.75f,   1,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0 },
        { 20,   0.75f,   1,  1,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0 },
        { 30,   1.00f,   1,  1,  1,  0,  0,  0,  0,  0,  0,  0,  0,  0 },
        { 40,   1.00f,   1,  1,  1,  1,  0,  0,  0,  0,  0,  0,  0,  0 },
        { 50,   0.50f,   0,  1,  0,  0,  0,  1,  0,  0,  0,  0,  0,  0 },
        { 65,   1.20f,   0,  1,  1,  0,  0,  1,  0,  0,  0,  0,  1,  0 },
        { 75,   0.75f,   1,  0,  0,  0,  1,  1,  0,  0,  0,  0,  1,  1 },
        { 90,   0.50f,   1,  0,  0,  1,  0,  1,  0,  0,  0,  0,  1,  1 },
        {105,   0.50f,   1,  0,  0,  1,  0,  1,  0,  0,  0,  0,  1,  1 },
        {125,   0.50f,   1,  0,  0,  1,  0,  1,  0,  0,  0,  0,  1,  1 },
        {145,   0.50f,   1,  0,  0,  1,  0,  1,  0,  0,  0,  0,  1,  1 },
        {165,   0.50f,   1,  0,  0,  1,  0,  1,  0,  0,  0,  0,  1,  1 },
        {200,   0.50f,   1,  0,  0,  1,  0,  1,  0,  0,  0,  0,  1,  1 },
        {210,   0.50f,   1,  0,  0,  1,  0,  1,  0,  0,  0,  0,  1,  1 },
        {230,   0.50f,   1,  0,  0,  1,  0,  1,  0,  0,  0,  0,  1,  1 },
        {240,   0.50f,   1,  0,  0,  1,  0,  1,  0,  0,  0,  0,  1,  1 },
        { -1,   0.50f,   1,  0,  0,  1,  0,  1,  0,  0,  0,  0,  1,  1 },
    };
}
