using System;
using System.Text;

namespace Adventure.Net;


public class WordWrap : IOutputFormatter
{
    public string Format(string text)
    {
        // if the text contains line feeds or tabs assume that it is pre-formatted
        if (text.Contains("\n") || text.Contains("\t"))
        {
            return text.Replace("\t", "  ");
        }

        StringBuilder sb = new StringBuilder();
        string[] words = text.Split(' ');
        int charsInLine = 0;

        for (int i = 0; i < words.Length; i++)
        {
            sb.AppendFormat("{0} ", words[i]);

            charsInLine += words[i].Length + 1;

            if (i < words.Length - 1)
            {
                if (charsInLine + words[i + 1].Length >= 120)
                {
                    sb.Append(Environment.NewLine);
                    charsInLine = 0;
                }
            }
        }

        return sb.ToString().Trim();
    }
}
