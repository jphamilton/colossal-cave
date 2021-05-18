//using System;
//using System.Collections.Generic;
//using System.Linq;
//using Adventure.Net.Verbs;

//namespace Adventure.Net
//{
//    public class UserInput
//    {
//        public InputResult Parse(string input)
//        {
//            var result = new InputResult();

//            //var tokenizer = new InputTokenizer();
//            //var tokens = tokenizer.Tokenize(input);

//            //if (tokens.Count == 0)
//            //{
//            //    result.Action = () =>
//            //    {
//            //        Context.Parser.Print(Messages.DoNotUnderstand);
//            //        return true;
//            //    };
//            //    return result;
//            //}

//            //// there can be more than one match for verbs like "switch"
//            //// which has one class that handles "switch on" and another 
//            //// class that handles "switch off"
//            //var possibleVerbs = VerbList.GetVerbsByName(tokens[0]);

//            //if (possibleVerbs.Count == 0)
//            //{
//            //    result.Verb = new NullVerb();
//            //    result.Action = ErrorAction(Messages.VerbNotRecognized);
//            //    return result;
//            //}

//            //if (possibleVerbs.Count == 1)
//            //{
//            //    result.Verb = possibleVerbs.First();
//            //}

//            //// remove verb token
//            //tokens.RemoveAt(0);

//            //var grammarTokens = new List<string>();
//            //var hasPreposition = false;
//            //var itemsParsed = new List<INamed>();
//            //var partialUnderstanding = false;

//            //foreach (string token in tokens)
//            //{
//            //    partialUnderstanding = result.Verb != null && itemsParsed.Any();

//            //    var itemsWithName = Objects.WithName(token);

//            //    var itemsInScope = (
//            //        from o in itemsWithName
//            //        where o.InScope
//            //        select o
//            //    ).ToList();

//            //    bool hasObject = result.Objects.Count > 0;

//            //    if (!hasObject)
//            //    {
//            //        var rooms = Rooms.WithName(token);
                    
//            //        foreach (var room in rooms)
//            //        {
//            //            itemsInScope.Add(room);
//            //        }
//            //    }

//            //    if (itemsInScope.Count == 0)
//            //    {
//            //        // parsing something like "go south"
//            //        bool isDirection = possibleVerbs.Count == 1 && Compass.Directions.Contains(token);
//            //        bool isPreposition = Prepositions.Contains(token);
//            //        bool isVerb = VerbList.GetVerbByName(token) != null;

//            //        if (isDirection && !isPreposition) // a direction other than "in"
//            //        {
//            //            if (!itemsParsed.Any() && token == tokens.Last())
//            //            {
//            //                // go south, leave east, etc.
//            //                possibleVerbs.Clear();
//            //                possibleVerbs.Add(VerbList.GetVerbByName(token));
//            //            }
//            //            else
//            //            {

//            //                // copying Inform output here (e.g. I only understood you as far as wanting to take the south)
//            //                var direction = VerbList.GetVerbByName(token);
//            //                result.Action = ErrorAction(Messages.PartialUnderstanding(result.Verb, direction));
//            //                return result;
//            //            }
                        
//            //        }
//            //        else if (isPreposition)
//            //        {
//            //            hasPreposition = true;
//            //            grammarTokens.Add(token);
//            //            result.Preposition = token;
//            //        }
                   
                    
//            //        else if (isVerb && partialUnderstanding)
//            //        {
//            //            result.Action = ErrorAction(Messages.PartialUnderstanding(result.Verb, itemsParsed.Last()));
//            //            return result;
//            //        }

//            //        else if (isVerb) // but verb has already specified - Inform will print this nonsense message
//            //        {
//            //            // extra verbs in input
//            //            //var verb = VerbList.GetVerbByName(token);
//            //            result.Action = ErrorAction(Messages.CantSeeObject);
//            //            return result;
//            //        }

//            //        else if (token == K.ALL)
//            //        {
//            //            grammarTokens.Add(token);
//            //            result.IsAll = true;
//            //        }
//            //        else if (token == K.EXCEPT)
//            //        {
//            //            if (!result.IsAll && !result.Objects.Any())
//            //            {
//            //                result.Action = ErrorAction(Messages.CantSeeObject);
//            //                return result;
//            //            }
//            //            result.IsExcept = true;
//            //        }
//            //        else if (itemsWithName.Any())
//            //        {
//            //            // item is in command but not in scope
//            //            result.Action = ErrorAction(Messages.CantSeeObject);
//            //            return result;
//            //        }
//            //        else if (partialUnderstanding)
//            //        {
//            //            result.Action = ErrorAction(Messages.PartialUnderstanding(result.Verb, result.Objects.First()));
//            //            return result;
//            //        }
//            //        //else
//            //        else if (token == tokens.Last())
//            //        {
                       
//            //            result.Action = ErrorAction(Messages.CantSeeObject);
//            //            return result;
//            //        }
//            //    }
//            //    else
//            //    {
//            //        // need to implement "Which do you mean, the red cape or the black cape?" type behavior here
//            //        Item item;
//            //        var ofInterest = itemsInScope.Where(x => x.InScope).ToList();

//            //        if (ofInterest.Count > 1)
//            //        {
//            //            item = ofInterest.FirstOrDefault(x => x.InInventory);
//            //        }
//            //        else
//            //        {
//            //            item = ofInterest.FirstOrDefault();
//            //        }

//            //        //-------------------------------------------------------------------------------------

//            //        bool isIndirectObject = hasPreposition && hasObject;

//            //        if (item == null)
//            //        {
//            //            result.Action = ErrorAction(Messages.CantSeeObject);
//            //            return result;
//            //        }

//            //        if (isIndirectObject)
//            //        {
//            //            grammarTokens.Add(K.INDIRECT_OBJECT_TOKEN);
//            //            result.IndirectObject = item;
//            //        }
//            //        else if (result.IsExcept)
//            //        {
//            //            result.Exceptions.Add(item);
//            //        }
//            //        else
//            //        {
//            //            if (!grammarTokens.Contains(K.OBJECT_TOKEN))
//            //            {
//            //                grammarTokens.Add(K.OBJECT_TOKEN);
//            //            }

//            //            if (!result.Objects.Contains(item))
//            //            {
//            //                result.Objects.Add(item);
//            //                itemsParsed.Add(item);
//            //            }
//            //        }
//            //    }

//            //}

//            //result.Pregrammar = string.Join(" ", grammarTokens.ToArray());

//            //var grammarBuilder = new GrammarBuilder(grammarTokens);
//            //var grammars = grammarBuilder.Build();

//            //FindVerb(result, possibleVerbs, grammars);

//            //if ((result.IsAll || result.Objects.Count > 1) && !result.Verb.AllowsMulti)
//            //{
//            //    return DoesNotAllowMulti();
//            //}

//            //if (result.Grammar == null)
//            //{
//            //    if (partialUnderstanding)
//            //    {
//            //        result.Action = ErrorAction(Messages.PartialUnderstanding(result.Verb, itemsParsed.Last()));
//            //        return result;
//            //    }

//            //    var incomplete = new IncompleteInput();
//            //    incomplete.Handle(result);
//            //}

//            //if (result.IsAll)
//            //{
//            //    if (result.ObjectsMustBeHeld)
//            //    {
//            //        result.Objects = Inventory.Items.Reverse().ToList();
//            //    }
//            //    else
//            //    {
//            //        result.Objects = (
//            //            from o in CurrentRoom.ObjectsInScope()
//            //            where !Inventory.Contains(o)
//            //            select o
//            //        ).ToList();
//            //    }
//            //}

//            //if (result.IsExcept)
//            //{
//            //    if (!result.Exceptions.Any())
//            //    {
//            //        return WhatDoYouWantToDo(result);
//            //    }

//            //    result.Exceptions.ForEach(x => result.Objects.Remove(x));
//            //}

//            return result;
//        }

//        private static bool IsPreposition(IEnumerable<string> tokens, string token)
//        {
//            if (!Prepositions.Contains(token))
//            {
//                return false;
//            }

//            if (token != tokens.Last())
//            {
//                return true;
//            }

//            // handle "in" (e.g. go in) and commands that end in "on" (e.g. turn lamp on)
//            return false;
//        }

//        private static InputResult WhatDoYouWantToDo(InputResult result)
//        {
//            var verb = result.Verb.Name;

//            return new InputResult
//            {
//                Action = () =>
//                {
//                    Context.Parser.Print($"What do you want to {verb}?");
//                    return true;
//                }
//            };
//        }

//        private static InputResult DoesNotAllowMulti()
//        {
//            return new InputResult
//            {
//                Action = () =>
//                {
//                    Context.Parser.Print($"You can't use multiple objects with that verb.");
//                    return true;
//                }
//            };
//        }

//        private void FindVerb(InputResult result, IEnumerable<Verb> possibleVerbs, IEnumerable<string> grammars)
//        {
//            foreach (var verb in possibleVerbs)
//            {
//                if (FindGrammar(result, verb, grammars))
//                {
//                    result.Verb = verb;
//                    return;
//                }
//            }
//        }

//        private bool FindGrammar(InputResult result, Verb verb, IEnumerable<string> grammars)
//        {
//            foreach (var possibleGrammar in grammars)
//            {
//                var matchedGrammar = verb.Grammars.FirstOrDefault(x => x.Format == possibleGrammar);

//                if (matchedGrammar != null)
//                {
//                    result.Grammar = matchedGrammar;
//                    return true;
//                }
//            }

//            return false;
//        }

//        public static Func<bool> ErrorAction(string error)
//        {
//            return () =>
//                {
//                    Context.Parser.Print(error);
//                    return true;
//                };
//        }
//    }
//}
