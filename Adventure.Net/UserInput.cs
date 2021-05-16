﻿using System;
using System.Collections.Generic;
using System.Linq;
using Adventure.Net.Verbs;

namespace Adventure.Net
{
    public class UserInput
    {
        public InputResult Parse(string input)
        {
            var result = new InputResult();

            var tokenizer = new InputTokenizer();
            var tokens = tokenizer.Tokenize(input);

            if (tokens.Count == 0)
            {
                result.Action = () =>
                {
                    Context.Parser.Print(Library.DoNotUnderstand);
                    return true;
                };
                return result;
            }

            // there can be more than one match for verbs like "switch"
            // which has one class that handles "switch on" and another 
            // class that handles "switch off"
            var possibleVerbs = VerbList.GetVerbsByName(tokens[0]);

            if (possibleVerbs.Count == 0)
            {
                result.Verb = new NullVerb();
                result.Action = ErrorAction(Library.VerbNotRecognized);
                return result;
            }

            if (possibleVerbs.Count == 1)
            {
                result.Verb = possibleVerbs.First();
            }

            // remove verb token
            tokens.RemoveAt(0);

            var grammarTokens = new List<string>();
            bool hasPreposition = false;

            foreach (string token in tokens)
            {
                // var objects = Objects.WithName(token);
                var objects = (
                    from o in Items.WithName(token)
                    where CurrentRoom.ObjectsInScope().Contains(o)
                    select o
                ).ToList();

                bool hasObject = result.Objects.Count > 0;

                if (!hasObject)
                {
                    var rooms = Rooms.WithName(token);
                    foreach (var room in rooms)
                    {
                        objects.Add(room);
                    }
                }

                if (objects.Count == 0)
                {
                    bool isDirection = possibleVerbs.Count == 1 &&
                                       Compass.Directions.Contains(token) &&
                                       result.Objects.Count == 0;
                    bool isPreposition = Prepositions.Contains(token);

                    if (isDirection)
                    {
                        possibleVerbs.Clear();
                        possibleVerbs.Add(VerbList.GetVerbByName(token));
                    }
                    else if (isPreposition)
                    {
                        hasPreposition = true;
                        grammarTokens.Add(token);
                        result.Preposition = token;
                    }
                    else if (token == K.ALL)
                    {
                        grammarTokens.Add(token);
                        result.IsAll = true;
                    }
                    else if (token == K.EXCEPT)
                    {
                        if (!result.IsAll && !result.Objects.Any())
                        {
                            result.Action = ErrorAction(Library.CantSeeObject);
                            return result;
                        }
                        result.IsExcept = true;
                    }
                    else
                    {
                        result.Action = ErrorAction(Library.CantSeeObject);
                        return result;
                    }
                }
                else
                {
                    // need to implement "Which do you mean, the red cape or the black cape?" type behavior here
                    Item obj;
                    var ofInterest = objects.Where(x => x.InScope).ToList();

                    if (ofInterest.Count > 1)
                    {
                        obj = ofInterest.FirstOrDefault(x => x.InInventory);
                    }
                    else
                    {
                        obj = ofInterest.FirstOrDefault();
                    }

                    //-------------------------------------------------------------------------------------

                    bool isIndirectObject = hasPreposition && hasObject;

                    if (obj == null)
                    {
                        result.Action = ErrorAction(Library.CantSeeObject);
                        return result;
                    }

                    if (isIndirectObject)
                    {
                        grammarTokens.Add(K.INDIRECT_OBJECT_TOKEN);
                        result.IndirectObject = obj;
                    }
                    else if (result.IsExcept)
                    {
                        result.Exceptions.Add(obj);
                    }
                    else
                    {
                        if (!grammarTokens.Contains(K.OBJECT_TOKEN))
                            grammarTokens.Add(K.OBJECT_TOKEN);
                        if (!result.Objects.Contains(obj))
                            result.Objects.Add(obj);
                    }
                }


            }

            result.Pregrammar = string.Join(" ", grammarTokens.ToArray());

            var grammarBuilder = new GrammarBuilder(grammarTokens);
            var grammars = grammarBuilder.Build();

            FindVerb(result, possibleVerbs, grammars);

            if ((result.IsAll || result.Objects.Count > 1) && !result.Verb.AllowsMulti)
            {
                return DoesNotAllowMulti();
            }

            if (result.Grammar == null)
            {
                var incomplete = new IncompleteInput();
                incomplete.Handle(result);
            }

            if (result.IsAll)
            {
                if (result.ObjectsMustBeHeld)
                {
                    result.Objects = Inventory.Items.Reverse().ToList();
                }
                else
                {
                    // This is different from Inform 6 which will include scenery and static
                    // objects (resulting in the generation of ridiculous messages like
                    // "well house: that's hardly portable"
                    result.Objects = (
                        from o in CurrentRoom.ObjectsInScope()
                        where o != CurrentRoom.Location && !o.IsScenery && !o.IsStatic
                        && !Inventory.Contains(o)
                        select o
                    ).ToList();
                }
            }

            if (result.IsExcept)
            {
                if (!result.Exceptions.Any())
                {
                    return WhatDoYouWantToDo(result);
                }

                result.Exceptions.ForEach(x => result.Objects.Remove(x));
            }

            return result;
        }

        private static InputResult WhatDoYouWantToDo(InputResult result)
        {
            var verb = result.Verb.Name;

            return new InputResult
            {
                Action = () =>
                {
                    Context.Parser.Print($"What do you want to {verb}?");
                    return true;
                }
            };
        }

        private static InputResult DoesNotAllowMulti()
        {
            return new InputResult
            {
                Action = () =>
                {
                    Context.Parser.Print($"You can't use multiple objects with that verb.");
                    return true;
                }
            };
        }

        private void FindVerb(InputResult result, IEnumerable<Verb> possibleVerbs, IEnumerable<string> grammars)
        {
            foreach (var verb in possibleVerbs)
            {
                if (FindGrammar(result, verb, grammars))
                {
                    result.Verb = verb;
                    return;
                }
            }
        }

        private bool FindGrammar(InputResult result, Verb verb, IEnumerable<string> grammars)
        {
            foreach (var possibleGrammar in grammars)
            {
                var matchedGrammar = verb.Grammars.FirstOrDefault(x => x.Format == possibleGrammar);

                if (matchedGrammar != null)
                {
                    result.Grammar = matchedGrammar;
                    return true;
                }
            }

            return false;
        }

        public static Func<bool> ErrorAction(string error)
        {
            return () =>
                {
                    Context.Parser.Print(error);
                    return true;
                };
        }
    }
}
