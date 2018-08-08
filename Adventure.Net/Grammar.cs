using System;
using System.Collections.Generic;
using System.Linq;

namespace Adventure.Net
{
    public class Grammar
    {
        public string Format { get; private set; }
        public Func<bool> Action { get; set; }
        public string Preposition { get; set; }
        private IList<string> tokens;

        public const string Empty = "";

        public Grammar(string format)
        {
            Format = format;

            foreach (var token in Tokens)
            {
                if (Prepositions.Contains(token))
                {
                    Preposition = token;
                    break;
                }
            }
        }

        public bool IsMulti
        {
            get { return Format.Contains("<multi>"); }
        }

        public bool IsMultiHeld
        {
            get { return Format.Contains("<multiheld>"); }
        }


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