using Adventure.Net.Extensions;
using Adventure.Net.Verbs;
using System.Collections.Generic;
using System.Linq;

namespace Adventure.Net
{

    public class CommandLineParser
    {
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

            // remove verb token
            tokens.RemoveAt(0);

            return Parse(verb, tokens);
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

                else if ((result.Verb is IDirectionProxy) && token.IsDirection() && !result.Objects.Any()) // distinguish between "go south", "put bottle down", "close up grate"
                {
                    var v = token.ToVerb();
                    result.Ordered.Add(v);
                    result.Verb = v;
                }

                //else if ((result.Verb is Enter || result.Verb is Go) && token.IsDirection())
                //{
                //    result.Objects.Add(VerbList.GetVerbByName(token));
                //}

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
                        return result;
                    }

                    var multi = new List<Item>();

                    if (verb.Multi)
                    {
                        multi.AddRange(CurrentRoom.ObjectsInRoom());
                    }

                    if (verb.MultiHeld)
                    {
                        multi.AddRange(Inventory.Items);

                        /*  
                        Special Case for multiheld commands where only 1 item is in inventory.
                            
                          > drop all
                          (the small lantern)
                          Dropped.

                        */

                        // TODO: this needs to be handled differently - whatever is done for Eat (held)
                        if (multi.Count == 1)
                        {
                            result.PreOutput.Add($"(the {multi.Single().Name})");
                        }
                    }

                    result.Objects.AddRange(multi);

                }
                else if (token == K.EXCEPT && (verb.Multi || verb.MultiHeld) && lastToken == K.ALL)
                {
                    var index = tokens.IndexOf(token) + 1;
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

                        obj = GetObject(result, t);

                        if (obj == null)
                        {
                            result.Error = Messages.CantSeeObject;
                            return result;
                        }

                        except.Add(obj);
                    }

                    result.Objects = result.Objects.Where(x => !except.Contains(x)).ToList();

                    return result;
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

        private CommandLineParserResult Parse(Verb verb, string input)
        {
            var tokenizer = new InputTokenizer();
            var tokens = tokenizer.Tokenize(input);
            return Parse(verb, tokens);
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
