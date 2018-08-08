using System;
using System.Linq;

namespace Adventure.Net
{
    public class IncompleteInput
    {
        private Library L;

        public void Handle(InputResult inputResult)
        {
            L = new Library();

            if (inputResult.Preposition.HasValue())
            {
                if (inputResult.Objects.Count == 1)
                {
                    OneObject(inputResult);
                }

            }
            else
            {
                inputResult.Grammar = inputResult.Verb.Grammars.First();
                if (inputResult.Preposition.HasValue())
                {
                    // dupe code from block above
                    if (inputResult.Objects.Count == 1)
                    {
                        OneObject(inputResult);
                    }
                    else if (inputResult.Objects.Count > 1)
                    {
                        MultipleObjects(inputResult);
                    }
                    else
                    {
                        NoObject(inputResult);
                    }
                }
            }

            //if (inputResult.Grammar == null)
            //{
            //    throw new Exception("What do we do with no Grammar?");
            //}

            //return false;
        }

        private void OneObject(InputResult inputResult)
        {
            var obj = inputResult.Objects.Single();
            inputResult.Grammar = inputResult.Verb.Grammars.FirstOrDefault(x => x.Preposition == inputResult.Preposition);
            string msg = "What do you want to {0} the {1} {2}?".F(inputResult.Verb.Name, obj.Name, inputResult.Preposition);
            Context.Output.Print(msg);

            string reply = Context.CommandPrompt.GetInput();
            if (string.IsNullOrEmpty(reply))
            {
                inputResult.Action = UserInput.ErrorAction(L.DoNotUnderstand);
            }

            var tokenizer = new InputTokenizer();
            var tokens = tokenizer.Tokenize(reply);

            //TODO: this should fail in some cases
            string input = null;

            if (!tokens.StartsWithVerb())
            {
                input = "{0} {1} {2} {3}".F(inputResult.Verb.Name, obj.Synonyms[0], inputResult.Preposition, reply);
            }
            else
            {
                input = reply;
            }

            inputResult.ParserResults.AddRange(Context.Parser.Parse(input));
            inputResult.Handled = true;
        }

        private void MultipleObjects(InputResult inputResult)
        {
            inputResult.ParserResults.Add(L.DidntUnderstandSentence);
            inputResult.Handled = true;
        }

        private void NoObject(InputResult inputResult)
        {
            string msg = "What do you want to {0}?".F(inputResult.Verb.Name);
            Context.Output.Print(msg);

            string reply = Context.CommandPrompt.GetInput();
            if (string.IsNullOrEmpty(reply))
            {
                inputResult.Action = UserInput.ErrorAction(L.DoNotUnderstand);
            }

            var tokenizer = new InputTokenizer();
            var tokens = tokenizer.Tokenize(reply);

            string input = null;

            if (!tokens.StartsWithVerb())
            {
                input = "{0} {1}".F(inputResult.Verb.Name, reply);
            }
            else
            {
                input = reply;
            }

            Context.Parser.Parse(input);
            //inputResult.ParserResults.AddRange();
            inputResult.Handled = true;
        }

    }
}