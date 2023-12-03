using System.Collections.Generic;

namespace Adventure.Net;

public class TokenizedInput : List<string>
{

}

public class InputTokenizer
{
    public TokenizedInput Tokenize(string input)
    {
        var result = new TokenizedInput();

        if (input != null)
        {
            foreach (string token in input.ToLower().Replace(',', ' ').Split(' '))
            {
                if (IgnoredWords.Contains(token))
                {
                    continue;
                }

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
