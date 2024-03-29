using System.IO;

namespace Adventure.Net;

public static class CommandPrompt
{
    private static TextWriter output;
    private static TextReader input;

    public static void Initialize(TextWriter output, TextReader input)
    {
        CommandPrompt.output = output;
        CommandPrompt.input = input;
    }

    public static string GetInput()
    {
        output.Write("\n> ");
        return input.ReadLine()?.Trim();
    }

    public static void FakeInput(string text)
    {
        input = new StringReader(text);
    }
}