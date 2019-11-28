using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ArrayExtensions {

    public static bool Contains<T>(this T[] t, T element)
    {
        foreach(T e in t)
        {
            if (e.Equals(element)) return true;
        }
        return false;
    }
}
