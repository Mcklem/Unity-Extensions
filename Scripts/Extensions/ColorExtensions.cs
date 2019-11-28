using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ColorExtensions {

	public static Color32 Clone(this Color32 color)
    {
        return new Color32(color.r, color.g, color.b, color.a);
    }

    public static Color Clone(this Color color)
    {
        return new Color(color.r, color.g, color.b, color.a);
    }
}
