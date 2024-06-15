using System;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Adventure.Net;

[ExcludeFromCodeCoverage]
public class WordWrap : IOutputFormatter
{
    private const int MaxLineLength = 120;

    public string Format(string text)
    {
        return IsPreformatted(text) ? HandlePreformatted(text) : WrapText(text);
    }

    private bool IsPreformatted(string text) => text.Contains('\n') || text.Contains('\t');

    private string HandlePreformatted(string text) => text.Replace("\t", "  ");

    private string WrapText(string text)
    {
        var sb = new StringBuilder();
        var words = text.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        int charsInLine = 0;

        foreach (var word in words)
        {
            if (charsInLine + word.Length >= MaxLineLength)
            {
                sb.AppendLine();
                charsInLine = 0;
            }

            if (charsInLine > 0)
            {
                sb.Append(' ');
                charsInLine++;
            }

            sb.Append(word);
            charsInLine += word.Length;
        }

        return sb.ToString();
    }
}
