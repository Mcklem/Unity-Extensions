﻿using System.Collections;
using System.Collections.Generic;
using System;

public static class DateExtensions {

    public static DateTime ConvertFromUnixTimestamp(this double timestamp)
    {
        DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
        return origin.AddSeconds(timestamp);
    }

    public static double ConvertToUnixTimestamp(this DateTime date)
    {
        DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
        TimeSpan diff = date.ToUniversalTime() - origin;
        return Math.Floor(diff.TotalSeconds);
    }
}
