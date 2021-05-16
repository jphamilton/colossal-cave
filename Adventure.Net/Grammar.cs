using System;
using System.Collections.Generic;
using System.Linq;

namespace Adventure.Net
{
    public class Grammar
    {
        public const string Empty = "";
        
        public string Format { get; private set; }
        public Func<bool> Action { get; set; }
        public string Preposition { get; set; }
        
        private IList<string> tokens;

        public Grammar(string format)
        {
            Format = format;

            IsMulti = Format.Contains("<multi>");
            IsMultiHeld = Format.Contains("<multiheld>");

            foreach (var token in Tokens)
            {
                if (Prepositions.Contains(token))
                {
                    Preposition = token;
                    break;
                }
            }
        }

        public bool IsMulti { get; }

        public bool IsMultiHeld { get; }

        private IList<string> Tokens
        {
            get
            {
                if (tokens == null)
                {
                    string format = Format;
                    format = format.ToLower();
                    format = format.Replace(',', ' ');
                    tokens = new List<string>(format.Split(' '));
                    while (tokens.Contains(""))
                        tokens.Remove("");
                }

                return tokens;
            }
        }
    }
}