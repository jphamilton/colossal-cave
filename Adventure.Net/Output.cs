using System;
using System.Collections.Generic;
using System.IO;

namespace Adventure.Net;

public static class Output
{
    private static TextWriter Target;
    private static IOutputFormatter Formatter;

    public static void Initialize(TextWriter destination, IOutputFormatter formatter)
    {
        Target = destination;
        Formatter = formatter;
    }

    public static void Bold(string text)
    {
        var currentColor = Console.ForegroundColor;
        Console.ForegroundColor = ConsoleColor.White;
        Print(text);
        Console.ForegroundColor = currentColor;
    }

    public static void Print(string text)
    {
        if (string.IsNullOrEmpty(text))
            return;

        string[] lines = text.Split('\n');

        foreach (string line in lines)
        {
            Target.WriteLine(Formatter.Format(line));
        }
    }

    public static void Print(IEnumerable<string> messages)
    {
        foreach (var message in messages)
        {
            Print(message);
        }
    }

    public static void Print(string format, params object[] arg)
    {
        Target.WriteLine(Formatter.Format(format), arg);
    }

    public static void PrintLine()
    {
        Target.WriteLine();
    }

    public static void Write(string text)
    {
        Target.Write(Formatter.Format(text));
    }

    public static string Buffer
    {
        get
        {
            return Target.ToString();
        }
    }

}