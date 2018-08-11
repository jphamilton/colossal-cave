using System;
using System.Collections.Generic;

namespace Adventure.Net
{
    public class Grammars : List<Grammar>
    {
        public void Add(string format, Func<bool> action)
        {
            Add(new Grammar(format) { Action = action });
        }
    }
}