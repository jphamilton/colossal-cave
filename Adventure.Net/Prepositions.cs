using System;
using System.Collections;
using System.Collections.Generic;

namespace Adventure.Net
{
    public enum Preposition
    {
        About,
        Against,
        Apart,
        At,
        Down,
        For,
        From,
        In,
        Inside,
        Into,
        Of,
        Off,
        On,
        Onto,
        Out,
        Over,
        Through,
        To,
        Top,
        Under,
        Up,
        With,
    }

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

        public static Preposition? Get(string value)
        {
            if (Enum.TryParse(value, true, out Preposition result))
            {
                return result;
            }

            return null;
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
