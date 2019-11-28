using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Reflection;

public static class ComponentExtensions {

    /// <summary>
    /// Clone a class by refletion.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="sourceComp"></param>
    /// <param name="targetComp"></param>
    public static void CopyClassValues<T, U>(this T sourceComp, U targetComp) where U: T
    {
        FieldInfo[] sourceFields = sourceComp.GetType().GetFields(BindingFlags.FlattenHierarchy | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
        for (int i = 0; i < sourceFields.Length; i++)
        {
            try
            {
                sourceFields[i].SetValue(targetComp, sourceFields[i].GetValue(sourceComp));
            }
            catch (Exception ex)
            {
                Debug.LogError("Error while copying class values" + ex.ToString());
            } 
        }
    }
}
