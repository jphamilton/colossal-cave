using System.Collections;
using System.Collections.Generic;

namespace Adventure.Net;

/// <summary>
/// Words ignored by the parser
/// </summary>
public static class IgnoredWords
{
    private static readonly List<string> ignored = [
       "", "a", "an", "and", "the"
    ];

    public static void Add(string word)
    {
        if (!ignored.Contains(word))
        {
            ignored.Add(word);
        }
    }

    public static bool Contains(string word)
    {
        return ignored.Contains(word);
    }
}
