using System;
using System.Collections;
using System.Collections.Generic;

namespace Adventure.Net
{
    public class Prepositions : IEnumerable<string>
    {
        private static readonly List<string> prepositions = new List<string>() { 
                "on", "off", "about", "for", "to", "with", "up", 
                "in", "into", "onto", "against", "out", 
                "over", "inside", "through", "under", "apart", 
                "down", "from", "top", "of", "at" 
        };

        public static void Add(string newPreposition)
        {
            if (!prepositions.Contains(newPreposition))
                prepositions.Add(newPreposition);
        }

        public static bool Contains(string preposition)
        {
            return prepositions.Contains(preposition);
        }

        public IEnumerator<string> GetEnumerator()
        {
            return prepositions.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
