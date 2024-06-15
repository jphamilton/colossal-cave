using System.Collections.Generic;
using System.IO;

namespace Adventure.Net;

public static class CommandPrompt
{
    private static TextWriter output;
    private static TextReader input;
    private static List<StringReader> fake = [];

    public static void Initialize(TextWriter output, TextReader input)
    {
        CommandPrompt.output = output;
        CommandPrompt.input = input;
        fake = [];
    }

    public static string GetInput()
    {
        output.Write("\n> ");
        
        if (fake.Count > 0)
        {
           input = fake[0];
           fake.RemoveAt(0);
        }

        return input.ReadLine()?.Trim();
    }

    public static void FakeInput(string text)
    {
        fake.Add(new StringReader(text));
    }
}