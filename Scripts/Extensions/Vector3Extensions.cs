using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Vector3Extensions {

    /// <summary>
    /// Return a random vector clamped between two vector values
    /// </summary>
    /// <param name="minValues"></param>
    /// <param name="maxValues"></param>
    /// <returns></returns>
    public static Vector3 GetRandomVector(Vector3 minValues, Vector3 maxValues)
    {
        return new Vector3(Random.Range(minValues.x, maxValues.x), Random.Range(minValues.y, maxValues.y), Random.Range(minValues.z, maxValues.z));
    }

    /// <summary>
    /// Returns the same vector but with each component as absolute value.
    /// </summary>
    /// <param name="vector"></param>
    /// <returns></returns>
    public static Vector3 AbsoluteValues(this Vector3 vector)
    {
        return new Vector3(Mathf.Abs(vector.x), Mathf.Abs(vector.y), Mathf.Abs(vector.z));
    }
}
