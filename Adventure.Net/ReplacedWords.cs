using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Adventure.Net;

/// <summary>
/// Words that are replaced during parsing. Used to normalize input.
/// </summary>
[ExcludeFromCodeCoverage]
public static class ReplacedWords
{
    private static readonly Dictionary<string, string> replaced = new() {
        {"everything", "all"},
        {"each", "all"},
        {"every", "all"},
        {"both", "all"},
        {"but", "except"},
        {"inside", "in"},
        {"into", "in"},
        {"onto", "on"},
        {"outside", "out"},
        {"underneath", "under"},
        {"beneath", "under"},
        {"below", "under"},
        {"using", "with"}, // Zork 1 source replaces "using", "through", and "thru" with this as well -- but I don't get it???
        {"n", "north"},
        {"s", "south"},
        {"e" , "east"},
        {"w" , "west"},
        {"d" , "down"},
        {"u" , "up"},
        {"nw" , "northwest"},
        {"ne" , "northeast"},
        {"sw" , "southwest"},
        {"se" , "southeast"},
    };

    public static string ReplacementFor(string word)
    {
        return replaced.TryGetValue(word, out var value) ? value : throw new Exception($"There is no replacement word for [{word}]");
    }

    public static void Add(string word, string replacement)
    {
        if (!replaced.ContainsKey(word))
        {
            replaced.Add(word, replacement);
        }
    }

    public static bool Contains(string word)
    {
        return replaced.ContainsKey(word);
    }
}
