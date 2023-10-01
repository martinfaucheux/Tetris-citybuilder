using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TextUtils
{
    public static string Colorize(string text, Color color) => (
        $"<color=#{ColorUtility.ToHtmlStringRGBA(color)}>{text}</color>"
    );
}
