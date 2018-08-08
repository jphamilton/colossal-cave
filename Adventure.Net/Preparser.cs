using System.Collections.Generic;

namespace Adventure.Net
{
    public class Preparser
    {
        private Library L = new Library();
        public string Error { get; private set; }
        public IList<Object> Objects { get; private set; }
        public Object IndirectObject { get; private set; }
        public IList<Object> Exceptions { get; private set; }
        public string Pregrammar { get; private set; }
        public IList<Verb> PossibleVerbs { get; private set; }

        public Preparser()
        {
            Objects = new List<Object>();
            Exceptions = new List<Object>();
        }

        public bool Parse(string input)
        {
            InputTokenizer tokenizer = new InputTokenizer();

            var tokens = tokenizer.Tokenize(input);

            if (tokens.Count == 0)
            {
                Error = L.DoNotUnderstand;
                return false;
            }

            IList<Verb> possibleVerbs = VerbList.GetVerbsByName(tokens[0]);

            if (possibleVerbs.Count == 0)
            {
                Error = L.VerbNotRecognized;
                return false;
            }

            // remove verb token
            tokens.RemoveAt(0);

            var grammarTokens = new List<string>();
            bool hasPreposition = false;
            bool isException = false;

            foreach (string token in tokens)
            {
                var obj = Adventure.Net.Objects.GetByName(token);

                if (obj == null)
                {
                    if (Prepositions.Contains(token))
                    {
                        hasPreposition = true;
                        grammarTokens.Add(token);
                    }
                    else if (token == "all")
                    {
                        grammarTokens.Add(token);
                    }
                    else if (token == "except")
                    {
                        isException = true;
                    }
                    else
                    {
                        Error = L.DoNotUnderstand;
                        return false;
                    }
                }
                else
                {
                    if (hasPreposition && Objects.Count > 0)
                    {
                        grammarTokens.Add("y");
                        IndirectObject = obj;
                    }
                    else if (isException)
                    {
                        Exceptions.Add(obj);
                    }
                    else
                    {
                        if (!grammarTokens.Contains("x"))
                            grammarTokens.Add("x");
                        Objects.Add(obj);
                    }
                }


            }

            Pregrammar = string.Join(" ", grammarTokens.ToArray());

            var grammarBuilder = new GrammarBuilder(grammarTokens);
            var grammars = grammarBuilder.Build();

            return true;
        }
    }
}
