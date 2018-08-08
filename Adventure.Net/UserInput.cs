using System;
using System.Collections.Generic;
using System.Linq;
using Adventure.Net.Verbs;

namespace Adventure.Net
{
    public class UserInput
    {
        private Library L = new Library();


        public InputResult Parse(string input)
        {
            var result = new InputResult();

            var tokenizer = new InputTokenizer();
            var tokens = tokenizer.Tokenize(input);

            Action removeVerbToken = () => tokens.RemoveAt(0);

            if (tokens.Count == 0)
            {
                result.Action = () =>
                {
                    Context.Parser.Print(L.DoNotUnderstand);
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
                result.Action = ErrorAction(L.VerbNotRecognized);
                return result;
            }
            
            if (possibleVerbs.Count == 1)
            {
                result.Verb = possibleVerbs.First();
            }
            //else { NOT sure what to do about multiple possible verbs here}


            // remove verb token
            removeVerbToken();

            var grammarTokens = new List<string>();
            bool hasPreposition = false;

            foreach (string token in tokens)
            {
                bool hasObject = result.Objects.Count > 0;

                var objects = Objects.WithName(token);
                
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
                       // result.Objects.AddRange(L.ObjectsInScope());
                        grammarTokens.Add(token);
                        result.IsAll = true;
                    }
                    else if (token == K.EXCEPT)
                    {
                        if (!result.IsAll && !result.Objects.Any())
                        {
                            result.Action = ErrorAction(L.CantSeeObject);
                            return result;
                        }
                        result.IsExcept = true;
                    }
                    else
                    {
                        if (result.IsPartial)
                        {
                            string partial = String.Format("I only understood you as far as wanting to {0} the {1}.", possibleVerbs[0].Name, result.Objects[0].Name);
                            result.Action = ErrorAction(partial);
                            return result;
                        }
                        
                        result.Action = ErrorAction(L.CantSeeObject);
                        return result;
                    }
                }
                else
                {
                    // need to implement "Which do you mean, the red cape or the black cape?" type behavior here
                    Object obj;
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
                        result.Action = ErrorAction(L.CantSeeObject);
                        return result;
                    }
                    
                    if (isIndirectObject)
                    {
                        grammarTokens.Add(K.INDIRECT_OBJECT_TOKEN);
                        result.IndirectObject = obj;
                    }
                    else if (result.IsExcept)
                    {
                        //result.Objects.Remove(obj);
                        result.Exceptions.Add(obj);
                    }
                    else
                    {
                        if (!grammarTokens.Contains(K.OBJECT_TOKEN))
                            grammarTokens.Add(K.OBJECT_TOKEN);
                        if (!result.Objects.Contains(obj))
                            result.Objects.Add(obj);
                        result.IsPartial = true;
                    }
                }

               
            }

            result.Pregrammar = string.Join(" ", grammarTokens.ToArray());

            var grammarBuilder = new GrammarBuilder(grammarTokens);
            var grammars = grammarBuilder.Build();

            FindVerb(result, possibleVerbs, grammars);

            if (result.Grammar == null)
            {
                var incomplete = new IncompleteInput();
                incomplete.Handle(result);
            }
            
            if (result.IsAll)
            {
                if (result.ObjectsMustBeHeld)
                {
                    result.Objects = result.Objects.Where(x => x.InInventory).ToList();
                }
                else
                {
                    result.Objects = L.ObjectsInScope();
                }
            }

            if (result.IsExcept)
            {
                result.Exceptions.ForEach(x => result.Objects.Remove(x));
            }

            return result;
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
