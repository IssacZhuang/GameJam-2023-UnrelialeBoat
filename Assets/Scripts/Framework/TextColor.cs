using System;
using System.Runtime.CompilerServices;

public static class TextColor
{
    public const string ColorRed = "<color=#ff0000ff>";
    public const string ColorGreen = "<color=#00ff00ff>";
    public const string ColorBlue = "<color=#0000ffff>";
    public const string ColorYellow = "<color=#ffff00ff>";
    public const string ColorWhite = "<color=#ffffffff>";
    public const string Postfix = "</color>";

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string Red(string text)
    {
        return string.Concat(ColorRed, text, Postfix);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string Green(string text)
    {
        return string.Concat(ColorGreen, text, Postfix);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string Blue(string text)
    {
        return string.Concat(ColorBlue, text, Postfix);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string Yellow(string text)
    {
        return string.Concat(ColorYellow, text, Postfix);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string White(string text)
    {
        return string.Concat(ColorWhite, text, Postfix);
    }
}
