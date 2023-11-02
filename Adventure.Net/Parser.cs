using Adventure.Net.Actions;
using Adventure.Net.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Adventure.Net;


public partial class Parser
{
    public ParserResult Parse(string input)
    {
        var tokens = SanitizeInput(input);

        if (tokens.Count == 0)
        {
            return new ParserResult { Error = Messages.DoNotUnderstand };
        }

        var verbToken = tokens[0];
        var verb = verbToken.ToVerb();

        if (verb == null)
        {
            return new ParserResult { Error = Messages.VerbNotRecognized };
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

        var result = Parse(verb, verbToken, tokens);

        ValidateResult(result);

        return result;
    }

    private TokenizedInput SanitizeInput(string input)
    {
        var tokenizer = new InputTokenizer();
        return tokenizer.Tokenize(input);
    }

    private ParserResult Parse(Verb verb, string verbToken, TokenizedInput tokens)
    {
        var result = new ParserResult
        {
            Verb = verb,
            Tokens = tokens
        };

        var lastToken = "";

        foreach (string token in tokens)
        {
            var obj = GetObject(result, token);

            if (obj is Skip)
            {
                continue;
            }

            if (result.Objects.Contains(obj) && !result.IsAll)
            {
                // e.g. "take the shiny brass lamp" - all object tokens refer to the same object
                continue;
            }

            if (obj != null)
            {
                if (obj.InScope || !result.Verb.InScopeOnly)
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

                        if (result.IsAll)
                        {
                            // e.g. "put all in cage" - prevent putting the cage into itself
                            result.Objects.Remove(obj);
                        }
                    }
                }
                else if (obj is MultipleObjectsFound found)
                {
                    return ResolveMultipleObjects(verb, found);
                }
                else
                {
                    result.Error = Messages.CantSeeObject;
                    return result;
                }
            }

            // distinguish between prepositions and movement - "go south", "put bottle down", "close up grate"
            else if ((result.Verb is IDirectional) && token.IsDirection() && !result.Objects.Any())
            {
                var v = token.ToVerb();
                result.Ordered.Add(v);
                result.Verb = v;
            }

            else if (token.IsPreposition())
            {
                // e.g. Drop has the synonym "throw", so "throw bottle" will just drop it.
                // However, "throw axe at dwarf" is redirected in Drop to ThrowAt. We need
                // to bypass this redirect and try to remap to the appropriate verb.
                var remapped = Verbs.Get($"{verbToken} {token}");
                if (remapped != null)
                {
                    result.Verb = remapped;
                }

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
                IList<Object> objectsInRoom = null;

                if (verb.Multi)
                {
                    objectsInRoom = GetObjectsInRoom();

                    multi.AddRange(objectsInRoom);
                }

                if (verb.MultiHeld)
                {
                    multi.AddRange(Inventory.Items);
                }

                // if object count is only 1, we don't add it so it can be handled in the verb using implict
                // messages e.g. (the small bottle) - unless there is only 1 object in the room
                if (multi.Count > 1 || objectsInRoom?.Count == 1)
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

    private ParserResult HandleExcept(ParserResult result, TokenizedInput tokens, string currentToken)
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

    private static Object GetObject(ParserResult result, string token)
    {
        // objects may have the same synonyms, so multiple items could be returned here
        var objects = (
            from o in Objects.WithName(token)
            where !o.Absent && result.Verb.InScopeOnly ? o.InScope : true
            select o
        ).ToList();

        if (objects.Count > 1)
        {
            if (result.Verb.MultiHeld)
            {
                objects = objects.Where(x => x.InInventory).ToList();
            }
            else
            {
                var notHeld = objects.Where(o => !o.InInventory).ToList();

                if (notHeld.Count > 0)
                {
                    objects = notHeld;
                }
            }
            
        }

        if (objects.Count == 1)
        {
            var obj = objects[0];

            if (obj.InScope || !result.Verb.InScopeOnly)
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

    private ParserResult ResolveMultipleObjects(Verb verb, MultipleObjectsFound multiple)
    {
        Output.Print($"Which do you mean, {multiple.Objects.DisplayList(definiteArticle: true, "or")}?");
        return GetInput(verb);
    }

    private ParserResult CheckForPossiblePartial(Verb verb)
    {
        // one-word command or partial command
        var verbType = verb.GetType();
        var expects = verbType.GetMethod("Expects", Array.Empty<Type>());

        if (expects != null)
        {
            // are implicit conditions satisfied?
            if (verb.Multi)
            {
                var objects = GetObjectsInRoom();

                if (objects.Count != 1)
                {
                    expects = null;
                }
            }
            else if (verb.MultiHeld && Inventory.Count != 1)
            {
                expects = null;
            }
        }
        

        if (expects == null)
        {
            Output.Print($"What do you want to {verb.Name}?");
            return GetInput(verb);
        }

        return null;
    }

    private IList<Object> GetObjectsInRoom()
    {
        return (
            from o in CurrentRoom.ObjectsInRoom()
            where !o.Absent && !o.Animate && !o.Static && !o.Scenery
            select o
        ).ToList();
    }

    private ParserResult GetInput(Verb verb)
    {
        var response = CommandPrompt.GetInput();

        var tokens = SanitizeInput(response);

        if (tokens.Count == 0)
        {
            return new ParserResult { Error = Messages.DoNotUnderstand };
        }

        var verbToken = tokens[0];
        if (verbToken.ToVerb() != null)
        {
            // a new command was entered instead of a partial response
            return Parse(string.Join(' ', tokens));
        }

        // parse original verb with the entered tokens
        return Parse(verb, verbToken, tokens);
    }

    private void ValidateResult(ParserResult result)
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
                $"What do you want to {verb.Name} the {result.Objects[0]} {prep}?";

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
                result.Error = input.Error ?? Messages.DidntUnderstandSentence;
            }
        }

        if (expects == null)
        {
            if (parameters.Key.Count == 1 && verb.AcceptedPrepositions.Count > 0)
            {
                // possible partial command entry
                foreach (var prep in verb.AcceptedPrepositions)
                {
                    var key = $"obj.{prep}.obj";

                    if (verb.GetHandler(key) != null)
                    {
                        // "unlock grate" will unlock the grate if player is hold only the key,
                        // but "put batteries" will not try to insert the batteries into itself.
                        if (Inventory.Count == 1 && !result.Objects.Contains(Inventory.Items[0]))
                        {
                            var first = Inventory.Items[0];
                            Output.Print($"({prep} the {first.Name})");
                            result.Preposition = prep;
                            result.IndirectObject = first;
                            expects = verb.GetHandler(result);

                            if (expects != null)
                            {
                                result.Expects = expects;
                                return;
                            }
                        }

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
                    result.Error = Messages.PartialUnderstanding(result.Verb, result.Objects[0]);
                }

            }
            else if (parameters.Key.Count == 3)
            {
                // possible partial command entry
                if (verb.GetHandler("obj") != null)
                {
                    result.Error = Messages.PartialUnderstanding(result.Verb, result.Objects[0]);
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
