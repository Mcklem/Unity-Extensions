using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StringExtensions {

    static readonly Dictionary<string,string> superscripts = new Dictionary<string, string>(){ { "0", "⁰" }, { "1", "¹" }, { "2", "²" }, { "3", "³" }, { "4", "⁴" }, { "5", "⁵" }, { "6", "⁶" }, { "7", "⁷" }, { "8", "⁸" }, { "9", "⁹" }, { "+", "⁺" }, { "-", "⁻" }, { "=", "⁼" }, { "(", "⁽" }, { ")", "⁾" }, { "n", "ⁿ" } };
    static readonly Dictionary<string, string> subscripts =  new Dictionary<string, string>(){ { "0", "₀" }, { "1", "₁" }, { "2", "₂" }, { "3", "₃" }, { "4", "₄" }, { "5", "₅" }, { "6", "₆" }, { "7", "₇" }, { "8", "₈" }, { "9", "₉" }, { "+", "₊" }, { "-", "₋" }, { "=", "₌" }, { "(", "₍" }, { ")", "₎" } };

    public static string RemoveSpecialCharacters(this string str)
    {
        StringBuilder sb = new StringBuilder();
        foreach (char c in str)
        {
            if ((c >= '0' && c <= '9') || (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z') || c == '.' || c == '_')
            {
                sb.Append(c);
            }
        }
        return sb.ToString();
    }

    public static bool IsEmail(this string inputEmail)
    {
        if (string.IsNullOrEmpty(inputEmail)) return false;
        string strRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
              @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
              @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
        Regex re = new Regex(strRegex);
        if (re.IsMatch(inputEmail)) return true;
        else return false;
    }

    /// <summary>
    /// Text length must be between min and max, both inclusive.
    /// </summary>
    /// <param name="text"></param>
    /// <param name="min"></param>
    /// <param name="max"></param>
    /// <returns></returns>
    public static bool RequiredLength(this string text, int min, int max)
    {
        return text.Length <= max && text.Length >= min;
    }

	/// <summary>
    /// Convert a "#RRGGBBAA" string to a Color32.
    /// </summary>
    /// <param name="hexColor"></param>
    /// <returns></returns>
    public static Color32 ToColor(this string hexColor)
    {
        Color color = new Color();
        ColorUtility.TryParseHtmlString(hexColor, out color);
        return color;
    }

    public static string ToSuperscript(this string text)
    {
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < text.Length; i++)
        {
            string character = text[i].ToString();
            if (superscripts.ContainsKey(character)) sb.Append(superscripts[character]);
            else sb.Append(character);
        }
        return sb.ToString();
    }

    public static string ToSubscript(this string text)
    {
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < text.Length; i++)
        {
            string character = text[i].ToString();
            if (subscripts.ContainsKey(character)) sb.Append(subscripts[character]);
            else sb.Append(character);
        }
        return sb.ToString();
    }
	
}
