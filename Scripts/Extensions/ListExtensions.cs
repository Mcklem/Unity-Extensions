using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ListExtensions {

    /// <summary>
    /// Returns a list of type T contained in the list
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="U"></typeparam>
    /// <param name="list"></param>
    /// <returns></returns>
	public static List<T> GetListFromType<T>(this IList list)
    {
        List<T> result = new List<T>();
        foreach(object o in list)
        {
            if (o is T) result.Add((T)o);
        }
        return result;
    }

    /// <summary>
    /// Returns each component if is found in GameObject list
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="gameObjects"></param>
    /// <returns></returns>
    public static List<T> GetEachComponent<T>(this IList<GameObject> gameObjects) where T : Component
    {
        List<T> list = new List<T>();
        T pointer = null;
        foreach (GameObject gameObject in gameObjects)
        {
            pointer = gameObject.GetComponent<T>();
            if (pointer) list.Add(pointer);
        }
        return list;
    }
}
