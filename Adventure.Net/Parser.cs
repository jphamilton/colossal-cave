using Adventure.Net.ActionRoutines;
using Adventure.Net.Extensions;
using Adventure.Net.Things;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System;

namespace Adventure.Net;

public class Parser
{
    public ParserResult Parse(string input, ParserResult previous = null)
    {
        var parsed = Preparse(input, previous?.Parsed);
        return new ParserResult(parsed);
    }

    public Parsed Preparse(string input, Parsed previous = null)
    {
        var tokenizer = new InputTokenizer();
        var tokens = tokenizer.Tokenize(input);

        if (tokens.Count == 0)
        {
            return Error(Messages.DoNotUnderstand);
        }

        var pr = CreatedParsed(previous, tokens, out int index);

        if (pr.Error.HasValue())
        {
            return pr;
        }

        if (pr.PossibleRoutines.Count == 1 && pr.PossibleRoutines[0] is ForwardTokens)
        {
            // e.g. save/restore and token is file name - routine will process tokens directly
            return pr;
        }

        Debug.Assert(pr.PossibleRoutines.Count > 0, "No routines found for verb");

        var remainingTokens = tokens.Count > index ? tokens[index..] : new TokenizedInput([]);

        if (remainingTokens.Count > 0)
        {
            pr = GetObjects(pr, remainingTokens);
        }

        if (pr.Error.HasValue())
        {
            return pr;
        }

        // swap indirect object with object if necessary (e.g. "put down lamp" -> "put lamp down")
        TryObjectIndirectSwap(pr);

        // can only have one indirect object
        if (pr.IndirectObjects.Count > 1)
        {
            pr.Error = Messages.DidntUnderstandSentence;
            TryParialUnderstanding(pr);
            return pr;
        }

        // implict take
        if (ImplicitTaker.CanTake(pr))
        {
            var routine = pr.PossibleRoutines[0];
            var r1 = routine.Requires[0];

            var flags = new List<O> { O.Held, O.MultiHeld, O.Multi, O.MultiExcept/*, O.MultiInside */};

            if (flags.Contains(r1) && !ImplicitTaker.Take(pr, pr.Objects.First()))
            {
                return pr;
            }
        }

        // try to narrow the list of possible routines
        if (pr.PossibleRoutines.Count > 1 && !TryNarrowRoutines(pr))
        {
            pr.PossibleRoutines = [pr.PossibleRoutines[0]];

            if (pr.Objects.Count > 0)
            {
                pr.Preposition = pr.PossibleRoutines[0].Prepositions.FirstOrDefault();
            }

            pr.PartialMessage = WhatDoYouWant(pr);
            return pr;
        }

        // if have multiple objects that have matched a token, we need to narrow it to one
        if (!TryNarrowObjects(pr) || pr.Error.HasValue())
        {
            return pr;
        }

        // verify the objects we have are valid for the routine
        return Verify(pr);
    }

    private Parsed CreatedParsed(Parsed previous, TokenizedInput tokens, out int index)
    {
        var pr = new Parsed { Input = tokens };

        index = 0;

        var twoWordVerb = tokens.Count > 1 ? $"{tokens[0]} {tokens[1]}" : null;

        if (tokens.Count > 1 && Dictionary.IsVerb(twoWordVerb))
        {
            if (Dictionary.IsPreposition(tokens[1]))
            {
                // e.g. "put on" -> verb root is "put", verb token is "put on"
                pr.VerbRoot = tokens[0];
                index = 1;
            }
            else
            {
                // e.g. verb is like "avada kedavra"
                pr.VerbRoot = twoWordVerb;
                index = 2;
            }
            
            pr.VerbToken = twoWordVerb;
            tokens = tokens[[pr.VerbToken], 2..];
        }
        else if (Dictionary.LookUp(tokens[0], out WordFlags f1) && (f1 & WordFlags.Verb) != 0)
        {
            pr.VerbRoot = tokens[0];
            pr.VerbToken = tokens[0];
            pr.VerbIsAlsoPrep = (f1 & WordFlags.Preposition) != 0;
            index = 1;
        }
        else if (previous?.IsPartial == true)
        {
            // Previous command was partial and this command does not start with a verb
            var fromPartial = CreateParsedFromPrevious(previous, tokens);
            index = fromPartial.CurrentReadIndex;
            return fromPartial;
        }
        else
        {
            return new Parsed { Error = Messages.VerbNotRecognized };
        }

        string prep = null;
        int objects = 0;
        int indirectObjects = 0;

        for (int i = index; i < tokens.Count; i++)
        {
            if (Dictionary.LookUp(tokens[i], out WordFlags f2) || tokens[i] == "all")
            {
                if ((f2 & WordFlags.Preposition) != 0)
                {
                    // pr.Preposition is assigned later
                    prep = tokens[i];
                    continue;
                }

                if (prep == null)
                {
                    objects = 1;
                }
                else
                {
                    indirectObjects = 1;
                }
            }
        }

        var requires = objects + indirectObjects;

        var routines = Routines.Find(pr.VerbToken, prep, requires);

        if (routines.Count == 0)
        {
            return new Parsed { Error = Messages.CantSeeObject };
        }

        pr.PossibleRoutines = routines;

        // if all of the possible routines have no prepositions, just take the first one
        if (pr.PossibleRoutines.Count > 1 && !pr.PossibleRoutines.Any(x => x.Prepositions.Count > 0))
        {
            pr.PossibleRoutines = [pr.PossibleRoutines[0]];
        }

        if (pr.Preposition == null && pr.VerbIsAlsoPrep)
        {
            pr.Preposition = pr.VerbToken;
        }

        return pr;
    }

    private Parsed CreateParsedFromPrevious(Parsed previous, TokenizedInput tokens)
    {
        previous.PartialMessage = null;

        var pr = new Parsed();

        pr.Update(GetObjects(previous, tokens));

        if (!TryNarrowObjects(pr) || pr.Error.HasValue())
        {
            return pr;
        }

        return Verify(pr);
    }

    public Parsed GetObjects(Parsed pr, List<string> tokens)
    {
        var wordTokens = TokenizeWords(tokens);
        if (wordTokens.Count == 0)
        {
            return Error(Messages.CantSeeObject);
        }

        pr.WordTokens.AddRange(wordTokens);

        for (int i = pr.CurrentReadIndex; i < pr.WordTokens.Count; i++, pr.CurrentReadIndex++)
        {
            if (pr.IsError)
            {
                return pr;
            }

            var wordToken = pr.WordTokens[i];
            var inScopeObjects = ResolveInScopeObjects(pr);

#pragma warning disable IDE0011 // Add braces
            if (HandleObjectToken(pr, wordToken, inScopeObjects)) continue;
            if (HandleVerbOrPreposition(pr, wordToken)) continue;
            if (HandleSpecialWords(pr, wordToken, inScopeObjects)) continue;
#pragma warning restore IDE0011 // Add braces
        }

        return pr;
    }

    private static List<WordToken> TokenizeWords(List<string> tokens)
    {
        var result = new List<WordToken>();

        foreach (var token in tokens)
        {
            if (Dictionary.LookUp(token, out var wordFlags))
            {
                result.Add(new WordToken(token, wordFlags));
            }
            else if (token == "all" || token == "except")
            {
                result.Add(new WordToken(token, WordFlags.None));
            }
            else
            {
                return [];
            }
        }

        return result;
    }

    private List<Object> ResolveInScopeObjects(Parsed pr)
    {
        var wordToken = pr.WordTokens[pr.CurrentReadIndex];

        bool includeOutOfScope = false;
        O r = O.None;

        if (pr.PossibleRoutines.Count == 1)
        {
            includeOutOfScope = !pr.PossibleRoutines[0].InScopeOnly;

            var requires = pr.PossibleRoutines[0].Requires;

            if (requires.Count > 0)
            {
                r = pr.Preposition == null ? requires[0] : requires.Count > 1 ? requires[1] : O.None;
            }
        }

        var candidates = includeOutOfScope ? Objects.All : CurrentRoom.ObjectsInScope(false);

        if (!includeOutOfScope && r == O.Noun && !wordToken.IsDoor)
        {
            candidates.Add(Player.Location);
        }

        if (wordToken.IsObject || wordToken.IsDoor)
        {
            candidates = [.. candidates.Where(x => x.Name == wordToken.Name || x.Synonyms.Contains(wordToken.Name))];
        }

        return candidates;
    }

    private bool HandleObjectToken(Parsed pr, WordToken wordToken, List<Object> inScopeObjects)
    {
        if (!wordToken.IsObject && !wordToken.IsDoor)
        {
            return false;
        }

        Object obj = null;
        
        if (inScopeObjects.Count == 0)
        {
            pr.Error = Messages.CantSeeObject;
            
            if (!pr.IsAll && !pr.IsExcept && pr.Objects.Count > 0)
            {
                TryParialUnderstanding(pr);
            }

            return true;
        }

        // we might have unresolved objects or indirect objects (token matches more than one object)
        var unresolved = pr.Preposition == null ? pr.UnresolvedObjects : pr.UnresolvedIndirectObjects;

        if (inScopeObjects.Count == 1)
        {
            // we have matched a single object
            obj = inScopeObjects[0];
        }
        
        if (inScopeObjects.Count > 1)
        {
            if (!pr.IsExcept && inScopeObjects.Any(x => pr.Objects.Contains(x) || pr.IndirectObjects.Contains(x)))
            {
                return true; // synonyms already in list
            }
            
            // we might have 3 unresolved "hats", but the current object narrows it down to the "purple hat"
            var intersect = unresolved.Intersect(inScopeObjects).ToList();
            if (intersect.Count == 1)
            {
                obj = intersect[0];
            }
            else
            {
                unresolved.AddRange(inScopeObjects);
            }
        }

        if (obj != null)
        {
            if (unresolved.Contains(obj))
            {
                unresolved.Clear();
            }

            var target = pr.Preposition == null ? pr.Objects : pr.IndirectObjects;

            if (pr.IsExcept)
            {
                // e.g. take all except hat
                // remove exceptions from the object list
                pr.Objects = [.. pr.Objects.Where(x => x != obj)];
            }
            else
            {
                target.Add(obj);
            }
        }

        return true;
    }

    private bool HandleVerbOrPreposition(Parsed pr, WordToken wordToken)
    {
        if (!wordToken.IsVerb)
        {
            return false;
        }

        if (pr.PossibleRoutines.Any(x => x is Direction))
        {
            var direction = Routines.List.First(x => x.Verbs.Contains(wordToken.Name));
            pr.PossibleRoutines = [direction];
            return true;
        }

        if (wordToken.IsPreposition)
        {
            if (pr.Preposition != null)
            {
                pr.Error = Messages.CantSeeObject;
                return true;
            }
            pr.Preposition = wordToken.Name;
            return true;
        }

        if (pr.Routine != null)
        {
            // e.g. look west
            var direction = (Direction)Routines.List.First(x => x is Direction && x.Verbs.Contains(wordToken.Name));
            var obj = direction.ToObject();
            
            if (pr.Preposition == null)
            {
                pr.Objects.Add(obj);
            }

            return true;
        }

        return false;
    }

    private bool HandleSpecialWords(Parsed pr, WordToken wordToken, List<Object> inScopeObjects)
    {
        if (wordToken.IsPreposition)
        {
            pr.Preposition = wordToken.Name;
            return true;
        }

        if (wordToken.IsAll && pr.Objects.Count == 0)
        {
            foreach (var obj in inScopeObjects.Where(o => !o.Animate && !o.Static && !o.Scenery).ToList())
            {
                pr.Objects.Add(obj);
            }
            
            pr.IsAll = true;
            return true;
        }

        if (wordToken.IsExcept)
        {
            pr.IsExcept = true;
            return true;
        }

        if (wordToken.IsRoom)
        {
            pr.Objects = [.. Objects.All.Where(x => x is Room && x.Synonyms.Contains(wordToken.Name))];
            return true;
        }

        return false;
    }

    private static bool TryNarrowObjects(Parsed pr)
    {
        var routine = pr.PossibleRoutines[0];
        var objects = pr.Objects.ToList();
        var indirect = pr.IndirectObjects.FirstOrDefault();

        if (objects.Count > 0 && !pr.Preposition.HasValue())
        {
            pr.Preposition = routine.Prepositions.FirstOrDefault();
        }

        // r1 can be any type
        var r1 = routine.Requires.Count > 0 ? routine.Requires[0] : O.None;

        // r2 will only be Noun, Held, Animate or Topic
        var r2 = routine.Requires.Count > 1 ? routine.Requires[1] : O.None;

        if (pr.UnresolvedObjects.Count == 0 && pr.UnresolvedIndirectObjects.Count == 0)
        {
            return true;
        }

        List<Object> ResolveObjects(List<Object> unresolved, O f)
        {
            if (pr.Routine != null && !pr.Routine.InScopeOnly)
            {
                return [.. unresolved];
            }
            else if (f == O.MultiHeld || f == O.Held)
            {
                return [.. unresolved.Where(Inventory.Contains)];
            }
            else
            {
                return [.. unresolved.Where(x => x.InScope && !Inventory.Contains(x))];
            }
        }

        bool WhichDoYouMean(List<Object> unresolved)
        {
            pr.PartialMessage = $"Which do you mean, {unresolved.DisplayList(definiteArticle: true, "or")}?";
            return false;
        }

        List<Object> resolved = null;

        if (pr.UnresolvedObjects.Count > 1)
        {
            resolved = ResolveObjects(pr.UnresolvedObjects, r1);

            if (resolved.Count > 1)
            {
                pr.UnresolvedObjects.Clear();
                return WhichDoYouMean(resolved);
            }

            pr.Aside = $"({resolved[0].DName})";
            pr.Objects.Add(resolved[0]);
        }

        if (pr.UnresolvedIndirectObjects.Count > 1)
        {
            resolved = ResolveObjects(pr.UnresolvedIndirectObjects, r1);

            if (resolved.Count > 1)
            {
                pr.UnresolvedIndirectObjects.Clear();
                return WhichDoYouMean(resolved);
            }
        }

        return true;
    }
    
    private Parsed Verify(Parsed pr)
    {
        var routine = pr.PossibleRoutines[0];
        var objects = pr.Objects.ToList();
        var indirect = pr.IndirectObjects.FirstOrDefault();
        var prep = pr.Preposition;
        bool empty = routine.Requires.Count == 0;

        O r1 = routine.Requires.Count > 0 ? routine.Requires[0] : O.None;
        O r2 = routine.Requires.Count > 1 ? routine.Requires[1] : O.None;

        if (empty && objects.Count == 0 || routine is Direction && objects.Count == 0)
        {
            return pr;
        }

#pragma warning disable IDE0011 // Add braces
        if (HandleReverseRoutine(pr, routine, objects, indirect, prep)) return pr;
        if (HandleAnimate(pr, r1)) return pr;
        if (HandleNoun(pr, routine, objects, r1, prep)) return pr;
        if (HandleMulti(pr, routine, objects, r1)) return pr;
        if (HandleHeld(pr, routine, objects, r1)) return pr;
        if (HandleMultiExcept(pr, routine, objects, indirect, prep, r1)) return pr;
        if (HandleMultiInside(pr, objects, indirect, prep, routine, r1)) return pr;
        if (HandleMultiHeld(pr, routine, objects, r1)) return pr;
        if (HandleImplicitIndirect(pr, routine, objects, indirect, prep, r2)) return pr;
#pragma warning restore IDE0011 // Add braces

        return pr;
    }

    private bool HandleReverseRoutine(Parsed pr, Routine routine, List<Object> objects, Object indirect, string prep)
    {
        if (!routine.Reverse)
        {
            return false;
        }

        if (objects.Count == 1 && indirect == null)
        {
            if (ImplicitTaker.TryGetImplicitObject(routine, prep, routine.Requires[1], out Object obj, out string aside))
            {
                pr.Aside = aside;
                pr.IndirectObjects = [objects[0]];
                pr.Objects = [obj];
                return true;
            }

            return false;
        }

        if (objects.Count == 2 && indirect == null)
        {
            pr.Objects = [objects[1]];
            pr.IndirectObjects = [objects[0]];
            return true;
        }

        return false;
    }

    private bool HandleAnimate(Parsed pr, O r1)
    {
        if (r1 == O.Animate)
        {
            var first = pr.Objects.Count > 0 ? pr.Objects.First() : null;
            var second = pr.IndirectObjects.Count > 0 ? pr.IndirectObjects.First() : null;

            if (pr.Objects.Count == 0)
            {
                pr.Objects = [Objects.Get<Player>()];
                pr.PartialMessage = WhatDoYouWant(pr);
                return true;
            }
            else if (first?.Animate != true && second?.Animate != true)
            {
                pr.Error = Messages.AnimateOnly;
                return true;
            }
        }

        return false;
    }

    private bool HandleNoun(Parsed pr, Routine routine, List<Object> objects, O r1, string prep)
    {
        if (r1 != O.Noun)
        {
            return false;
        }

        if (objects.Count == 0 && ImplicitTaker.TryGetImplicitObject(routine, prep, r1, out Object obj, out string aside))
        {
            pr.Aside = aside;
            pr.Objects = [obj];
            return true;
        }

        if (objects.Count > 1)
        {
            pr.Error = Messages.MultiNotAllowed;
            return true;
        }

        if (objects.Count == 0)
        {
            pr.PartialMessage = WhatDoYouWant(pr);
            return true;
        }

        return false;
    }

    private bool HandleMulti(Parsed pr, Routine routine, List<Object> objects, O r1)
    {
        if (r1 != O.Multi)
        {
            return false;
        }

        if (objects.Count == 0)
        {
            if (ImplicitTaker.TryGetImplicitObject(routine, pr.Preposition, r1, out Object obj, out string aside))
            {
                pr.Aside = aside;
                pr.Objects = [obj];
                return true;
            }

            pr.PartialMessage = WhatDoYouWant(pr);
            return true;
        }

        return false;
    }

    private bool HandleHeld(Parsed pr, Routine routine, List<Object> objects, O r1)
    {
        if (r1 != O.Held)
        {
            return false;
        }

        if (objects.Count == 0)
        {
            if (ImplicitTaker.TryGetImplicitObject(routine, pr.Preposition, r1, out Object obj, out string aside))
            {
                pr.Aside = aside;
                pr.Objects = [obj];
                return true;
            }

            pr.PartialMessage = WhatDoYouWant(pr);
            return true;
        }

        if (objects.Count > 1)
        {
            pr.Error = Messages.MultiNotAllowed;
            return true;
        }

        return false;
    }

    private bool HandleMultiExcept(Parsed pr, Routine routine, List<Object> objects, Object indirect, string prep, O r1)
    {
        if (r1 != O.MultiExcept)
        {
            return false;
        }

        if (!ValidateMultiExceptInside(pr, out string error, out string question))
        {
            if (error != null)
            {
                pr.Error = error;
            }
            else if (question != null)
            {
                pr.PartialMessage = question;
            }

            return true;
        }

        pr.Objects = [.. pr.Objects.Where(x => x != indirect)];
        ImplicitTaker.Take(pr, [.. pr.Objects]);
        pr.IndirectObjects = [indirect];

        return true;
    }

    private bool HandleMultiInside(Parsed pr, List<Object> objects, Object indirect, string prep, Routine routine, O r1)
    {
        if (r1 != O.MultiInside)
        {
            return false;
        }

        if (!ValidateMultiExceptInside(pr, out string error, out string question))
        {
            if (error != null)
            {
                pr.Error = error;
            }
            else if (question != null)
            {
                pr.PartialMessage = question;
            }

            return true;
        }

        return false;
    }

    private bool HandleMultiHeld(Parsed pr, Routine routine, List<Object> objects, O r1)
    {
        if (r1 != O.MultiHeld)
        {
            return false;
        }

        if (pr.IsAll)
        {
            var held = objects.Where(Inventory.Contains).ToList();
            pr.Objects = [.. held];
            objects = held;
        }

        if (objects.Count == 1 && pr.IsAll)
        {
            pr.Aside = $"({objects[0].DName})";
            return true;
        }
        else if (objects.Count == 1 && InContainer(objects[0], out Container container))
        {
            pr.Aside = $"(first taking {objects[0].DName} out of {container.DName})";
            return true;
        }
        else if (objects.Count == 0)
        {
            if (ImplicitTaker.TryGetImplicitObject(routine, pr.Preposition, r1, out Object obj, out string aside))
            {
                pr.Aside = aside;
                pr.Objects = [obj];
            }
            else
            {
                pr.PartialMessage = WhatDoYouWant(pr);
            }

            return true;
        }

        return false;
    }

    private bool HandleImplicitIndirect(Parsed pr, Routine routine, List<Object> objects, Object indirect, string prep, O r2)
    {
        if (r2 != O.Held || objects.Count != 1)
        {
            return false;
        }

        if (indirect == null)
        {
            var obj = routine.ImplicitObject(r2);
            if (obj != null)
            {
                pr.Aside = $"({prep} {obj.DName})";
                pr.IndirectObjects = [obj];
                return true;
            }
            else
            {
                pr.PartialMessage = WhatDoYouWant(pr);
                return true;
            }
        }

        ImplicitTaker.Take(pr, indirect);
        return true;
    }

    private bool ValidateMultiExceptInside(Parsed pr, out string error, out string question)
    {
        error = null;
        question = null;

        var routine = pr.PossibleRoutines[0];
        var empty = routine.Requires.Count == 0;
        var objects = pr.Objects.ToList();
        var indirect = pr.IndirectObjects.FirstOrDefault();
        var prep = pr.Preposition;

        if (!empty && objects.Count == 0 && indirect == null)
        {
            error = Messages.CantSeeObject;
        }
        else if (objects.Count == 1 && objects[0] == indirect)
        {
            error = $"You can't {routine.Verb} something {prep} itself.";
        }
        else if (objects.Count > 0 && indirect == null && prep != null)
        {
            var name = objects.Count > 1 ? "those things" : $"{objects[0].DName}";
            question = $"What do you want to {routine.Verbs[0]} {name} {prep}?";
        }

        return string.IsNullOrEmpty(error) && string.IsNullOrEmpty(question);
    }

    private static bool TryNarrowRoutines(Parsed pr)
    {
        if (pr.Objects.Count > 0)
        {
            if (pr.Objects.Count == 1)
            {
                ImplicitTaker.Take(pr, pr.Objects.First());
            }

            pr.PossibleRoutines = [.. pr.PossibleRoutines.Where(x => x.Requires.Count > 0)];
        }

        return pr.PossibleRoutines.Count == 1;
    }

    private static string WhatDoYouWant(Parsed pr)
    {
        var routine = pr.PossibleRoutines[0];
        var objects = pr.Objects;
        var prep = pr.Preposition ?? (objects.Count > 0 ? routine.Prepositions.FirstOrDefault() : null);

        var sb = new StringBuilder($"What do you want to {pr.VerbRoot}");

        if (objects.Count > 1)
        {
            sb.Append(" those things");
        }
        else if (objects.Count == 1)
        {
            var obj = pr.Objects.First();
            var target = obj is Player ? "yourself" : obj.DName;
            sb.Append($" {target}");
        }

        if (!string.IsNullOrEmpty(prep))
        {
            sb.Append($" {prep}");
        }

        sb.Append('?');

        return sb.ToString();
    }

    private static void TryObjectIndirectSwap(Parsed pr)
    {
        if (pr.Preposition.HasValue() && pr.Objects.Count == 0 && pr.IndirectObjects.Count > 0)
        {
            // e.g. turn on lamp -> turn lamp on
            pr.Objects = pr.IndirectObjects;
            pr.IndirectObjects = [];
        }
    }

    private static void TryParialUnderstanding(Parsed pr)
    {
        var routine = pr.PossibleRoutines[0];

        // these always require indirect objects
        if (routine.Requires.Count == 2 && pr.IndirectObjects.Count == 0)
        {
            return;
        }

        var objects = pr.Objects.ToList();
        var indirect = pr.IndirectObjects.FirstOrDefault();
        var prep = pr.Preposition;

        var sb = new StringBuilder($"I only understood you as far as wanting to {routine.Verb} ");

        if (prep != null && indirect == null)
        {
            sb.Append($"{prep} ");
        }

        if (objects.Count > 0)
        {
            sb.Append(objects.DisplayList(definiteArticle: true, "and"));
        }

        if (prep != null && indirect != null)
        {
            sb.Append($" {prep} {indirect.DName}");
        }

        sb.Append('.');

        pr.PartialMessage = sb.ToString();
    }

    private bool InContainer(Object obj, out Container container)
    {
        container = null;

        if (obj.Animate)
        {
            return false;
        }

        if (obj.Parent is Container c && Inventory.Contains(c) && c.Open)
        {
            container = c;
            return true;
        }

        return false;
    }

    private Parsed Error(string error)
    {
        return new Parsed { Error = error };
    }
}