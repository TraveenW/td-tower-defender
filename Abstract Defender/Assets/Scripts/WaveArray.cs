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
        { 10,   1.00f,   1,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0 },
        { 20,   1.00f,   1,  1,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0 },
        { 30,   1.30f,   1,  1,  0,  0,  0,  1,  0,  0,  0,  0,  0,  0 },
        { 40,   1.30f,   1,  1,  0,  0,  0,  0,  0,  0,  0,  1,  0,  0 },
        { 50,   0.50f,   1,  1,  0,  0,  1,  1,  0,  0,  1,  0,  0,  0 },
        { 65,   1.60f,   0,  1,  1,  0,  0,  1,  0,  0,  0,  0,  0,  0 },
        { 75,   1.00f,   1,  0,  0,  0,  1,  1,  0,  0,  0,  1,  0,  0 },
        { 90,   0.75f,   1,  0,  0,  1,  0,  0,  1,  0,  0,  1,  0,  0 },
        {105,   0.75f,   1,  1,  1,  0,  0,  1,  1,  0,  0,  1,  0,  0 },
        {125,   1.30f,   1,  1,  0,  0,  0,  1,  0,  1,  0,  1,  0,  0 },
        {145,   1.00f,   1,  1,  0,  0,  0,  1,  1,  0,  1,  0,  1,  0 },
        {147,   0.03f,   1,  0,  0,  0,  1,  0,  0,  0,  1,  0,  0,  0 },
        {165,   1.30f,   1,  0,  1,  0,  0,  1,  0,  1,  0,  0,  1,  0 },
        {200,   1.00f,   1,  0,  1,  0,  0,  1,  0,  0,  1,  0,  1,  0 },
        {210,   0.75f,   1,  0,  1,  0,  0,  1,  0,  0,  0,  0,  0,  0 },
        {230,   1.00f,   1,  0,  0,  1,  0,  1,  0,  1,  0,  0,  0,  0 },
        {240,   1.30f,   1,  0,  0,  1,  0,  1,  0,  1,  0,  0,  1,  0 },
        {242,   0.04f,   0,  1,  0,  0,  0,  1,  0,  0,  0,  1,  0,  0 },
        { -1,   1.30f,   0,  0,  0,  1,  0,  0,  1,  1,  0,  1,  0,  1 },
    };
}
