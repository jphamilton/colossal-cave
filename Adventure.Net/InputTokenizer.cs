using System.Collections.Generic;

namespace Adventure.Net
{
    public class InputTokenizer
    {
        public IList<string> Tokenize(string input)
        {
            var result = new List<string>();

            if (input != null)
            {
                input = input.ToLower();
                input = input.Replace(',', ' ');

                var tokens = input.Split(' ');

                foreach (string token in tokens)
                {
                    if (IgnoredWords.Contains(token))
                        continue;

                    if (ReplacedWords.Contains(token))
                    {
                        result.Add(ReplacedWords.ReplacementFor(token));
                    }
                    else
                    {
                        result.Add(token);
                    }
                }    
            }
           
            return result;
        }
    }
}
