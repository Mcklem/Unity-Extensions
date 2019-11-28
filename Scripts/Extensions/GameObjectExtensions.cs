using UnityEngine;

public static class GameObjectExtensions
{
    public static bool HasComponent<T>(this GameObject gob) where T : Component
    {
        return gob.GetComponent<T>() != null;
    }

}