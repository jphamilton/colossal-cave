using System;
using System.Collections.Generic;

namespace Adventure.Net
{
    public class Grammars : List<Grammar>
    {
        public void Add(string format, Func<bool> action)
        {
            if (format.IsMultiWord())
            {
                AddMultiWord(format, action);
            }
            else
            {
                Add(new Grammar(format) { Action = action });
            }
        }

        private void AddMultiWord(string format, Func<bool> action)
        {
            int start = format.IndexOf("[");
            int end = format.IndexOf("]", start);
            
            if (start >= 0 && end > start)
            {
                string pattern = format.Substring(start, end - start + 1);
                string temp = Strip(pattern, "[", "]");
                string[] words = temp.Split(',');

                foreach (string word in words)
                {
                    var single = format.Replace(pattern, word);
                    Add(new Grammar(single) { Action = action });
                }
            }
        }

        private string Strip(string input, params string[] values)
        {
            // not the best
            foreach (string value in values)
            {
                input = input.Replace(value, "");
            }

            return input;
        }
    }
}