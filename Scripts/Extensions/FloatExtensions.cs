using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class FloatExtensions {

    public static readonly double FI = 1.618033988749894848;

    /// <summary>
    /// Returns proportionally scaled value based on 'Golden Ratio'.
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
	public static float ToGoldenRatioProportion(this float value)
    {
        return (float) FI * value;
    }
}
