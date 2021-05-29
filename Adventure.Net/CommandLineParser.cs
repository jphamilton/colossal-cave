using Adventure.Net.Extensions;
using Adventure.Net.Verbs;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Adventure.Net
{

    public class CommandLineParser
    {
        private static CommandLineParserResult previous;

        public CommandLineParserResult Parse(string input)
        {
            var tokenizer = new InputTokenizer();
            var tokens = tokenizer.Tokenize(input);

            if (tokens.Count == 0)
            {
                return new CommandLineParserResult { Error = Messages.DoNotUnderstand };
            }

            var verb = tokens[0].ToVerb();

            if (verb == null)
            {
                // this allows the parser to handle partial response
                // > take
                // What do you want to take?
                //
                // > bottle
                // Taken.
                if (previous != null && previous.Verb != null)
                {
                    verb = previous.Verb;
                }
                else
                {
                    return new CommandLineParserResult { Error = Messages.VerbNotRecognized };
                }
            }
            else
            {
                // remove verb token
                tokens.RemoveAt(0);
            }


            // store result and return
            var result = Parse(verb, tokens);

            if (verb != null && tokens.Count == 0)
            {
                // check if this is a single word command
                var verbType = verb.GetType();
                var expects = verbType.GetMethod("Expects", new Type[] { });

                if (expects == null)
                {
                    result.Error = $"What do you want to {result.Verb.Name}?";
                }
            }

            // if previous command is not null, clear it, otherwise store it
            previous = previous != null ? null : result;

            return result;
        }

        private CommandLineParserResult Parse(Verb verb, TokenizedInput tokens)
        {
            var result = new CommandLineParserResult
            {
                Verb = verb
            };

            var lastToken = "";

            foreach (string token in tokens)
            {
                var obj = GetObject(result, token);

                if (obj != null)
                {
                    if (obj.InScope)
                    {
                        if (result.Preposition == null || !result.Objects.Any())
                        {
                            result.Ordered.Add(obj);
                            result.Objects.Add(obj);
                        }
                        else
                        {
                            result.Ordered.Add(obj);
                            result.IndirectObject = obj;
                        }
                    }
                    else
                    {
                        result.Error = Messages.CantSeeObject;
                        return result;
                    }
                }

                // distinguish between "go south", "put bottle down", "close up grate"
                else if ((result.Verb is IDirectionProxy) && token.IsDirection() && !result.Objects.Any()) 
                {
                    var v = token.ToVerb();
                    result.Ordered.Add(v);
                    result.Verb = v;
                }

                else if (token.IsPreposition())
                {
                    var p = Prepositions.Get(token);
                    result.Ordered.Add(p);
                    result.Preposition = p;
                }

                else if (token == K.ALL && !result.Objects.Any())
                {
                    result.IsAll = true;

                    if (!verb.Multi && !verb.MultiHeld)
                    {
                        result.Error = Messages.MultiNotAllowed;
                        //return result;
                        break;
                    }

                    var multi = new List<Item>();

                    if (verb.Multi)
                    {
                        multi.AddRange(CurrentRoom.ObjectsInRoom());
                    }

                    if (verb.MultiHeld)
                    {
                        multi.AddRange(Inventory.Items);
                    }

                    // if object count is only 1, we don't add it so it can be handled in the verb using implict
                    // messages e.g. (the small bottle)
                    if (multi.Count > 1)
                    {
                        result.Objects.AddRange(multi);
                    }

                }

                else if (token == K.EXCEPT && (verb.Multi || verb.MultiHeld) && lastToken == K.ALL)
                {
                    HandleExcept(result, tokens, token);
                    break;
                }

                else
                {
                    obj = result.Objects.FirstOrDefault();

                    if (obj != null && !result.IsAll)
                    {
                        result.Error = Messages.PartialUnderstanding(verb, obj);
                    }
                    else
                    {
                        result.Error = Messages.CantSeeObject;
                    }

                    return result;
                }

                lastToken = token;
            }

            return result;
        }

        private void HandleExcept(CommandLineParserResult result, TokenizedInput tokens, string currentToken)
        {
            var index = tokens.IndexOf(currentToken) + 1;

            if (index >= tokens.Count)
            {
                //result.Error = $"What do you want to {result.Verb.Name}?";
                return;
            }

            var except = new List<Item>();

            // process rest of the tokens as objects
            for (int i = index; i < tokens.Count; i++)
            {
                // TODO: handle multiple objects with same name
                var t = tokens[i];

                if (t.IsPreposition() && result.Preposition == null)
                {
                    result.Preposition = Prepositions.Get(t);
                    continue;
                }

                var obj = GetObject(result, t);

                if (obj == null)
                {
                    result.Error = Messages.CantSeeObject;
                    break;
                }

                except.Add(obj);
            }

            var success = !result.Error.HasValue();

            if (success)
            {
                result.Objects = result.Objects.Where(x => !except.Contains(x)).ToList();
            }

            return;
        }

        private Item GetObject(CommandLineParserResult result, string token)
        {
            //TODO: I have a feeling we are not done here

            // objects may have the same synonyms
            var objects = (
                from o in Objects.WithName(token)
                where o.InScope
                select o
            ).ToList();

            // special case: token refers to a Door which is handled as a Room
            var doors = (
                from r in Rooms.WithName(token)
                where r.InScope && r is Door
                select r
            ).ToList();

            objects.AddRange(doors);

            if (objects.Count == 1)
            {
                var obj = objects.First();

                if (obj.InScope)
                {
                    return obj;
                }
            }
            else  if (objects.Count > 1)
            {
                if (objects.Any(obj => !obj.InScope))
                {
                    return null;
                }
                else
                {
                    // TODO: implement "which do you mean?"
                }
            }

            return null;
        }
    }
}
