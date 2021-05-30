using Adventure.Net.Extensions;
using Adventure.Net.Verbs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Adventure.Net
{

    public class CommandLineParser
    {
        //private static CommandLineParserResult previous;

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
                return new CommandLineParserResult { Error = Messages.VerbNotRecognized };
            }

            tokens.RemoveAt(0);

            if (tokens.Count == 0)
            {
                var partial = CheckForPossiblePartial(verb);
                
                if (partial != null)
                {
                    return partial;
                }
            }

            // store result and return
            var result = Parse(verb, tokens);

            // validate result before command handler gets it
            // handle errors here too
            if (!result.Error.HasValue())
            {
                ValidateResult(result);
            }

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
                var obj = GetObject(token);

                if (obj != null)
                {
                    if (obj.InScope)
                    {
                        if (result.Preposition == null || !result.Objects.Any())
                        {
                            // handles commands like "put on coat", "put down book", etc.
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
                var next = tokens[i];

                if (next.IsPreposition() && result.Preposition == null)
                {
                    result.Preposition = Prepositions.Get(next);
                    continue;
                }

                var obj = GetObject(next);

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

        private Item GetObject(string token)
        {
            //TODO: I have a feeling we are not done here

            // objects may have the same synonyms, so multiple items could be returned here
            var objects = (
                from o in Objects.WithName(token)
                where o.InScope
                select o
            ).ToList();

            // Inform seems to favor objects not held here, which makes sense as you
            // wouldn't want to accidentally destroy an important game object resolving
            // ambiguous commands
            //
            // note: Colossal Cave, holding a bottle of water next to stream, Inform
            // will trigger a drink from the stream
            if (objects.Count > 1)
            {
                var notHeld = objects.Where(o => !o.InInventory).ToList();

                if (notHeld.Count > 0)
                {
                    objects = notHeld;
                }
            }

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
            else if (objects.Count > 1)
            {
                throw new NotImplementedException("Which do you mean?");
            }

            return null;
        }
    
        private CommandLineParserResult CheckForPossiblePartial(Verb verb)
        {
            // one-word command or partial command
            var verbType = verb.GetType();
            var expects = verbType.GetMethod("Expects", Array.Empty<Type>());

            if (expects == null)
            {
                Output.Print($"What do you want to {verb.Name}?");

                var response = CommandPrompt.GetInput();
                var tokenizer = new InputTokenizer();
                var tokens = tokenizer.Tokenize(response);

                if (tokens.Count == 0)
                {
                    return new CommandLineParserResult { Error = Messages.DoNotUnderstand };
                }

                if (tokens[0].ToVerb() != null)
                {
                    // a new command was entered instead of a partial response
                    return Parse(string.Join(' ', tokens));
                }

                // parse original verb with the entered tokens
                return Parse(verb, tokens);
            }

            return null;
        }

        private void ValidateResult(CommandLineParserResult result)
        {
            var call = new DynamicCall(result);
            var expects = new DynamicExpects(result.Verb, call);

            if (expects.Expects == null)
            {

                // this is just observed Inform 6 behavior
                if (result.Ordered.Count > 0)
                {
                    if (result.Ordered[0] is Item)
                    {
                        result.Error = Messages.PartialUnderstanding(result.Verb, result.Objects.First());
                    }
                    else
                    {
                        result.Error = Messages.CantSeeObject;
                    }

                }
            }

        }
    }
}
