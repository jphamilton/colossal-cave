using System.Collections;
using System.Collections.Generic;

namespace Adventure.Net
{
    /// <summary>
    /// Words ignored by the parser
    /// </summary>
    public class IgnoredWords : IEnumerable<string>
    {
        private static readonly List<string> ignored = new List<string>() {
           "", "a", "an", "and", "the"
        };

        public static void Add(string word)
        {
            if (!ignored.Contains(word))
                ignored.Add(word);
        }

        public static bool Contains(string word)
        {
            return ignored.Contains(word);
        }


        public IEnumerator<string> GetEnumerator()
        {
            return ignored.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
