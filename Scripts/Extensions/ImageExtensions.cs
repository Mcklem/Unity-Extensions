using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// DEPRECATED USE CROSSFADECOLOR AND CROSSFADE ALPHA FROM Graphic class (Parent of Image, Text...)
/// </summary>
public static class ImageExtensions {

    public static IEnumerator FadeColor(this Image image, Color32 to, float time, Action<Color32> onFadeUpdate)
    {
        float timer = 0f;
        float step = 0f;
        Color32 color = image.color.Clone();
        do
        {
            if (time != 0f) step = timer / time;
            else step = 1;

            int r = (short)Mathf.Lerp(color.r, to.r, step);
            int g = (short)Mathf.Lerp(color.g, to.g, step);
            int b = (short)Mathf.Lerp(color.b, to.b, step);
            int a = (short)Mathf.Lerp(color.a, to.a, step);
            onFadeUpdate(new Color32((byte)r, (byte)g, (byte)b, (byte)a));
            timer += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        while (timer < time);

    }

    public static IEnumerator FadeAlpha(this Image image, byte alpha, float time, Action<Color32> onFadeUpdate)
    {
        float timer = 0f;
        float step = 0f;
        Color32 color = image.color.Clone();
        Color32 to = image.color.Clone();
        to.a = alpha;
        do
        {
            if (time != 0f) step = timer / time;
            else step = 1;

            int r = (short)Mathf.Lerp(color.r, to.r, step);
            int g = (short)Mathf.Lerp(color.g, to.g, step);
            int b = (short)Mathf.Lerp(color.b, to.b, step);
            int a = (short)Mathf.Lerp(color.a, to.a, step);
            onFadeUpdate(new Color32((byte)r, (byte)g, (byte)b, (byte)a));
            timer += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        while (timer < time);

    }

}
