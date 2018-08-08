using System;
using System.Collections;
using System.Collections.Generic;

namespace Adventure.Net
{
    /// <summary>
    /// Words that are replaced during parsing. Used to normalize input.
    /// </summary>
    public class ReplacedWords : IEnumerable<string>
    {
        private static readonly Dictionary<string, string> replaced = new Dictionary<string, string>() {
            {",", " "},
            {"everything", "all"},
            {"but", "except"},
        };

        public static string ReplacementFor(string word)
        {
            return replaced.ContainsKey(word) ? replaced[word] : "";
        }

        public static void Add(string word, string replacement)
        {
            if (!replaced.ContainsKey(word))
                replaced.Add(word, replacement);
        }

        public static bool Contains(string word)
        {
            return replaced.ContainsKey(word);
        }

        public IEnumerator<string> GetEnumerator()
        {
            return replaced.Keys.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
