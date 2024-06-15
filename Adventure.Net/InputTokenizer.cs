using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Adventure.Net;

[DebuggerDisplay("{DebuggerDisplay,nq}")]
public class TokenizedInput : List<string>
{
    public string Input { get; set; }

    private string DebuggerDisplay => $"TokenizedInput [{string.Join(", ", this)}]";

    public TokenizedInput(IEnumerable<string> tokens) : base(tokens)
    {
    }

    public TokenizedInput this[Range range]
    {
        get
        {
            var (offset, length) = range.GetOffsetAndLength(Count);
            return new TokenizedInput(GetRange(offset, length));
        }
    }

    public TokenizedInput this[string[] prepends, Range range]
    {
        get
        {
            var (offset, length) = range.GetOffsetAndLength(Count);
            var result = new TokenizedInput(prepends);
            result.AddRange(GetRange(offset, length));
            return result;
        }
    }

    //public void RemoveToken(int index)
    //{
    //    if (index >= 0 && index < Count)
    //    {
    //        RemoveAt(index);
    //    }

    //    Input = string.Join(" ", this);
    //}

    //public void AddTokens(IEnumerable<string> tokens)
    //{
    //    if (tokens != null)
    //    {
    //        foreach (var token in tokens)
    //        {
    //            Add(token);
    //        }
    //    }

    //    Input = string.Join(" ", this);
    //}
}

public class InputTokenizer
{
    public TokenizedInput Tokenize(string input)
    {
        if (input == null)
        {
            return new TokenizedInput([]);
        }

        var tokens = input.Trim().ToLower().Replace(',', ' ').Split(' ').Select(t => t.Trim());
        
        var words = new List<string>();

        if (input != null)
        {
            foreach (string token in tokens)
            {
                if (IgnoredWords.Contains(token))
                {
                    continue;
                }

                if (ReplacedWords.Contains(token))
                {
                    words.Add(ReplacedWords.ReplacementFor(token));
                }
                else
                {
                    words.Add(token);
                }
            }
        }

        var result = new TokenizedInput(words);
        result.Input = string.Join(" ", words);

        return result;
    }
}
