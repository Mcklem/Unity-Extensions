using System.Collections.Generic;
using UnityEngine;

public static class TransformExtensions
{
    /// <summary>
    /// Return the first child who matches with name
    /// </summary>
    /// <param name="aParent"></param>
    /// <param name="name"></param>
    /// <param name="includeInactive"></param>
    /// <returns>Null if doesnt match with any child</returns>
    public static Transform FindDeepChild(this Transform aParent, string name, bool includeInactive = true)
    {
        var result = aParent.Find(name);
        if (result != null)
            return result;

        /*foreach (Transform child in aParent.gameObject.GetComponentsInChildren(typeof(Transform), includeInactive))
        {
            if (child.name == name)
                return child;   
        }*/

     
        foreach (Transform child in aParent)
        {
            result = child.FindDeepChild(name);
            if (result != null)
                return result;
        }
        return null;
    }

    /// <summary>
    /// Allign two transforms, setting the same position and rotation
    /// </summary>
    /// <param name="me"></param>
    /// <param name="other"></param>
    public static void AllignTo(this Transform me, Transform other)
    {
        me.position = other.position;
        me.rotation = me.rotation;
    }

    /// <summary>
    /// Generate a transform at the same position, rotation and scale
    /// </summary>
    /// <param name="me"></param>
    /// <returns></returns>
    public static Transform Clone(this Transform me, string rename = null) {
        string name = me.name;
        if (!string.IsNullOrEmpty(rename)) name = rename;
        GameObject clone = new GameObject(name);
        clone.transform.AllignTo(me);
        clone.transform.localScale = me.lossyScale;
        return clone.transform;
    }

    /// <summary>
    /// Return the nearest transform of the array.
    /// e.g new Transform[] { transform1 , transform2 ...}
    /// </summary>
    /// <param name="transform"></param>
    /// <param name="pivots"></param>
    /// <returns></returns>
    public static Transform GetClosestPivot(this Transform transform, Transform[] pivots)
    {
        Transform tMin = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = transform.position;
        foreach (Transform t in pivots)
        {
            float dist = Vector3.Distance(t.position, currentPos);
            if (dist < minDist)
            {
                tMin = t;
                minDist = dist;
            }
        }
        return tMin;
    }

    /// <summary>
    /// Return the nearest transform of the array.
    /// e.g new Transform[] { transform1 , transform2 ...}
    /// </summary>
    /// <param name="transform"></param>
    /// <param name="pivots"></param>
    /// <returns></returns>
    /*public static Transform GetFurthestPivot(this Transform transform, Transform[] pivots)
    {
        Transform tMin = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = transform.position;
        foreach (Transform t in pivots)
        {
            float dist = Vector3.Distance(t.position, currentPos);
            if (dist < minDist)
            {
                tMin = t;
                minDist = dist;
            }
        }
        return tMin;
    }*/

    /// <summary>
    /// Return the nearest transform of the list.
    /// </summary>
    /// <param name="transform"></param>
    /// <param name="pivots"></param>
    /// <returns></returns>
    public static Transform GetClosestPivot(this Transform transform, List<Transform> pivots)
    {
        return GetClosestPivot(transform, pivots.ToArray());
    }

    /// <summary>
    /// Returns the transform with the highest 'y' position
    /// </summary>
    /// <param name="transforms"></param>
    /// <returns></returns>
    public static Transform GetHigest(this List<Transform> transforms)
    {
        if (transforms.Count <= 0) return null;
        Transform highest = transforms[0];
        foreach (Transform t in transforms)
        {
            if (t.position.y > highest.position.y) highest = t;
        }
        return highest;
    }

    /// <summary>
    /// Returns the transform with the lowest 'y' position
    /// </summary>
    /// <param name="transforms"></param>
    /// <returns></returns>
    public static Transform GetLowest(this List<Transform> transforms)
    {
        if (transforms.Count <= 0) return null;
        Transform lowest = transforms[0];
        foreach (Transform t in transforms)
        {
            if (t.position.y < lowest.position.y) lowest = t;
        }
        return lowest;
    }

    /// <summary>
    /// Copies from other transform the position, rotation and local scale.
    /// </summary>
    /// <param name="transform"></param>
    /// <param name="otherTransform"></param>
    public static void CopyFrom(this Transform transform, Transform otherTransform)
    {
        transform.position = otherTransform.position;
        transform.rotation = otherTransform.rotation;
        transform.localScale = otherTransform.localScale;
    }

    /// <summary>
    /// Reset scale, rotation and position locally
    /// </summary>
    /// <param name="transform"></param>
    public static void ResetLocals(this Transform transform)
    {
        transform.localEulerAngles = Vector3.zero;
        transform.localPosition = Vector3.zero;
        transform.localScale = Vector3.one;
    }

    /// <summary>
    /// Reset transform to default values.
    /// </summary>
    /// <param name="transform"></param>
    public static void Reset(this Transform transform)
    {
        transform.position = Vector3.zero;
        transform.rotation = Quaternion.identity;
        transform.localScale = Vector3.one;
    }

    /// <summary>
    /// Destroy all children gameObjects.
    /// </summary>
    /// <param name="transform"></param>
    /// <param name="otherTransform"></param>
    public static void DestroyChildren(this Transform transform)
    {
        foreach (Transform child in transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }

}