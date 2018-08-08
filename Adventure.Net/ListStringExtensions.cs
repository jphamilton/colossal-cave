using System;
using System.Collections.Generic;

namespace Adventure.Net
{
    public static class ListStringExtensions
    {
        public static void Remove(this IList<string> input, IEnumerable<string> remove)
        {
            foreach (var item in remove)
            {
                input.Remove(item);
            }
        }

        public static bool IsEmpty(this IList<string> input)
        {
            foreach (string item in input)
            {
                if (!String.IsNullOrEmpty(item))
                    return false;
            }

            return true;
        }

        public static bool StartsWithVerb(this IList<string> words)
        {
            bool result = false;

            if (words.Count > 0)
                result = (VerbList.GetVerbByName(words[0]) != null);

            return result;
        }
    }
}
