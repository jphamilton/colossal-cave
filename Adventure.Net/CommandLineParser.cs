﻿using Adventure.Net.Extensions;
using Adventure.Net.Actions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Adventure.Net
{

    public partial class CommandLineParser
    {
        private class Skip : Object
        {
            public override void Initialize()
            {
                // no op
            }
        }


        public CommandLineParserResult Parse(string input)
        {
            var tokens = SanitizeInput(input);

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
                    ValidateResult(partial);
                    return partial;
                }
            }

            var result = Parse(verb, tokens);

            ValidateResult(result);

            return result;
        }

        private TokenizedInput SanitizeInput(string input)
        {
            var tokenizer = new InputTokenizer();
            var tokens = tokenizer.Tokenize(input);

            return tokens;
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

                if (obj is Skip)
                {
                    continue;
                }

                if (result.Objects.Contains(obj))
                {
                    // will happen for something like "take the brass lamp"
                    continue;
                }

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
                    else if (obj is MultipleObjectsFound)
                    {
                        return ResolveMultipleObjects(verb, (MultipleObjectsFound)obj);
                    }
                    else
                    {
                        result.Error = Messages.CantSeeObject;
                        return result;
                    }
                }

                // distinguish between prepositions and movement - "go south", "put bottle down", "close up grate"
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

                else if (token == "all" && !result.Objects.Any())
                {
                    result.IsAll = true;

                    if (!verb.Multi && !verb.MultiHeld)
                    {
                        result.Error = Messages.MultiNotAllowed;
                        break;
                    }

                    var multi = new List<Object>();

                    if (verb.Multi)
                    {
                        var objectsInRoom =
                            from o in CurrentRoom.ObjectsInRoom()
                            where !o.Animate
                            select o;

                        multi.AddRange(objectsInRoom);
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

                else if (token == "except" && (verb.Multi || verb.MultiHeld) && lastToken == "all")
                {
                    var except = HandleExcept(result, tokens, token);
                    
                    if (except != null)
                    {
                        return except;
                    }

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

        private CommandLineParserResult HandleExcept(CommandLineParserResult result, TokenizedInput tokens, string currentToken)
        {
            var index = tokens.IndexOf(currentToken) + 1;

            if (index >= tokens.Count)
            {
                return null;
            }

            var except = new List<Object>();

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

                var obj = GetObject(result, next);

                if (obj == null)
                {
                    result.Error = Messages.CantSeeObject;
                    return result;
                }
                else if (obj is MultipleObjectsFound)
                {
                    var input = GetInput(result.Verb);
                    
                    if (input.Error.HasValue())
                    {
                        return input;
                    }

                    if (input.Objects.Count > 0)
                    {
                        except.AddRange(input.Objects);
                    }
                }
                else
                {
                    except.Add(obj);
                }
            }

            result.Objects = result.Objects.Where(x => !except.Contains(x)).ToList();

            return null;
        }

        private Object GetObject(CommandLineParserResult result, string token)
        {
            //TODO: This is crazy train
            
            // objects may have the same synonyms, so multiple items could be returned here
            var objects = (
                from o in Objects.WithName(token)
                where result.Verb.InScopeOnly ? o.InScope : true
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
                var last = result.Ordered?.LastOrDefault();

                if (last != null && last is Object obj)
                {
                    if (!obj.Name.Contains(token) && !obj.Synonyms.Contains(token))
                    {
                        return new MultipleObjectsFound(objects);
                    }

                    return new Skip();
                }
                else
                {
                    return new MultipleObjectsFound(objects);
                }

            }

            return null;
        }

        private CommandLineParserResult ResolveMultipleObjects(Verb verb, MultipleObjectsFound multiple)
        {
            Output.Print($"Which do you mean, {multiple.List()}?");
            return GetInput(verb);
        }

        private CommandLineParserResult CheckForPossiblePartial(Verb verb)
        {
            // one-word command or partial command
            var verbType = verb.GetType();
            var expects = verbType.GetMethod("Expects", Array.Empty<Type>());

            if (expects == null)
            {
                Output.Print($"What do you want to {verb.Name}?");
                return GetInput(verb);
            }

            return null;
        }

        private CommandLineParserResult GetInput(Verb verb)
        {
            var response = CommandPrompt.GetInput();
            
            var tokens = SanitizeInput(response);

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

        private void ValidateResult(CommandLineParserResult result)
        {
            if (result.Error.HasValue())
            {
                return;
            }

            Parameters parameters = result;
            var verb = result.Verb;
            
            var expects = verb.GetHandler(result);

            void WhatDoYouWantToDo(Prep prep)
            {
                var message = result.Objects.Count > 1 ?
                    $"What do you want to {verb.Name} those things {prep}?" :
                    $"What do you want to {verb.Name} {result.Objects.First()} {prep}?";

                Output.Print(message);

                var input = GetInput(verb);

                if (input.Objects.Count == 1)
                {
                    result.Preposition = prep;
                    result.IndirectObject = input.Objects[0];
                    ValidateResult(result);
                }
                else
                {
                    result.Error = Messages.DidntUnderstandSentence;
                }
            }

            if (expects == null)
            {
                if (parameters.Key.Count == 1 && verb.AcceptedPrepositions.Count > 0)
                {
                    // possible partial command entry
                    foreach(var prep in verb.AcceptedPrepositions)
                    {
                        var key = $"obj.{prep}.obj";

                        if (verb.GetHandler(key) != null)
                        {
                            WhatDoYouWantToDo(prep);
                            return;
                        }
                    }

                    result.Error = Messages.PartialUnderstanding(result.Verb);
                }
                else if (parameters.Key.Count == 2)
                {
                    // possible partial command entry
                    var key = $"{parameters}.obj";
                    
                    if (verb.GetHandler(key) != null)
                    {
                        WhatDoYouWantToDo(result.Preposition);
                    }
                    else
                    {
                        result.Error = Messages.PartialUnderstanding(result.Verb, result.Objects.First());
                    }

                }
                else if (parameters.Key.Count == 3)
                {
                    // possible partial command entry
                    var key = $"obj";

                    if (verb.GetHandler(key) != null)
                    {
                        result.Error = Messages.PartialUnderstanding(result.Verb, result.Objects.First());
                    }
                }
                else if (result.Preposition != null)
                {
                    result.Error = Messages.CantSeeObject;
                }
                else
                {
                    result.Error = Messages.DidntUnderstandSentence;
                }
            }
            else
            {
                result.Expects = expects;
            }
        }
    }
}
