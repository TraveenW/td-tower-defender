using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartesianAndPolar : MonoBehaviour
{
    // Sets an angle to be between 0 and 2pi.
    // Angle must be given in radians.
    float LimitAngle(float o)
    {
        while (o < 0)
        {
            o += 2 * Mathf.PI;
        }
        while (o > 2 * Mathf.PI)
        {
            o -= 2 * Mathf.PI;
        }
        return o;
    }

    // Input: (x & y) Variables representing 2D Cartesian Coordinates 
    // Output: [r, o] Array representing 2D Polar Coordinates: radius and angle 
    // The angle is in degrees, between 0 and 360.
    public static float[] ConvertToPolar(float x, float y)
    {
        float[] polarCoOrds = { 0, 0 };
        var la = new CartesianAndPolar();
        
        polarCoOrds[0] = Mathf.Sqrt(x * x + y * y);
        polarCoOrds[1] = la.LimitAngle(Mathf.Atan2(y, x));
        polarCoOrds[1] = Mathf.Rad2Deg * polarCoOrds[1];
        return polarCoOrds;
    }

    // Input: [x, y] Array representing 2D Cartesian Coordinates 
    // Output: [r, o] Array representing 2D Polar Coordinates: radius and angle 
    // The angle is in degrees, between 0 and 360.
    public static float[] ConvertToPolar(float[] cartCoOrds)
    {
        float[] polarCoOrds = { 0, 0 };
        var la = new CartesianAndPolar();

        polarCoOrds[0] = Mathf.Sqrt(cartCoOrds[0] * cartCoOrds[0] + cartCoOrds[1] * cartCoOrds[1]);
        polarCoOrds[1] = la.LimitAngle(Mathf.Atan2(cartCoOrds[1], cartCoOrds[0]));
        polarCoOrds[1] = Mathf.Rad2Deg * polarCoOrds[1];
        return polarCoOrds;
    }

    // Input: (r & o) Variables representing 2D Polar Coordinates 
    // Output: [x, y] Array representing 2D Cartesian Coordinates 
    // The angle should be given in degrees. 
    public static float[] ConvertToCartesian(float r, float o)
    {
        float[] cartCoOrds = { 0, 0 };
        var la = new CartesianAndPolar();
        o = la.LimitAngle(Mathf.Deg2Rad * o);

        cartCoOrds[0] = r * Mathf.Cos(o);
        cartCoOrds[1] = r * Mathf.Sin(o);
        
        return cartCoOrds;
    }

    // Input: [r, o] Array representing 2D Polar Coordinates 
    // Output: [x, y] Array representing 2D Cartesian Coordinates 
    // The angle should be given in degrees. 
    public static float[] ConvertToCartesian(float[] polarCoOrds)
    {
        float[] cartCoOrds = { 0, 0 };
        var la = new CartesianAndPolar();
        polarCoOrds[1] = la.LimitAngle(Mathf.Deg2Rad * polarCoOrds[1]);

        cartCoOrds[0] = polarCoOrds[0] * Mathf.Cos(polarCoOrds[1]);
        cartCoOrds[1] = polarCoOrds[0] * Mathf.Sin(polarCoOrds[1]);

        return cartCoOrds;
    }
}
