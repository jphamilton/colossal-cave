using System;
using System.Collections.Generic;
using System.IO;

namespace Adventure.Net;

public static class Output
{
    private static TextWriter _target;
    private static IOutputFormatter _formatter;

    public static void Initialize(TextWriter destination, IOutputFormatter formatter)
    {
        _target = destination;
        _formatter = formatter;
    }

    public static void Bold(string text)
    {
        Console.Write($"\x1b[1m");
        Print(text);
        Console.Write($"\x1b[0m");
    }

    public static void Print(string message, bool direct = false)
    {
        if (string.IsNullOrEmpty(message))
        {
            return;
        }

        message = message.Replace("\n", Environment.NewLine);

        var formatted = OutputOverride(_formatter.Format(message));

        if (Context.Current != null && !direct)
        {
            Context.Current.Print(formatted);
        }
        else
        {
            _target.WriteLine(formatted);
        }
    }

    public static void Print(IEnumerable<string> messages)
    {
        foreach (var message in messages)
        {
            Print(message);
        }
    }

    private static Func<string, string> OutputOverride = (x) => x;

    private class OutputOverrider : IDisposable
    {
        public void Dispose()
        {
            OutputOverride = (x) => x;

            if (Context.Current != null)
            {
                Context.Current.PrintOverride = (x) => x;
            }
        }
    }

    public static IDisposable Override(Func<string,string> x)
    {
        OutputOverride = x;

        if (Context.Current != null)
        {
            Context.Current.PrintOverride = x;
        }

        return new OutputOverrider();
    }

    public static void PrintLine()
    {
        if (Context.Current != null)
        {
            Context.Current.Print("\r");
        }
        else
        {
            _target.WriteLine();
        }
    }

}