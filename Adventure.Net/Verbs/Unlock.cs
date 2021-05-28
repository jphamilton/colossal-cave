using Adventure.Net.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Adventure.Net.Verbs
{
    // TODO: implement
    public class Unlock : Verb
    {
        public Unlock()
        {
            Name = "unlock";
         //   Grammars.Add("<noun> with <held>", UnlockObject);
         //   Grammars.Add("<noun>", UnlockObject);
        }

        public bool Expects(Item obj)
        {
            if (!obj.IsLockable)
            {
                Print("That doesn't seem to be something you can unlock.");
            }
            else if (!obj.IsLocked)
            {
                Print("It's unlocked at the moment.");
            }
            else if (obj is Door) // TODO: need to refactor so this works for all lockable things
            {
                var door = obj as Door;
                return UnlockDoor(door, null);
            }
            else
            {
                return UnlockObject(obj);
            }

            return true;
        }

        public bool Expects(Item obj, Preposition prep, Item indirect)
        {
            if (obj is Door && prep == Preposition.With)
            {
                return UnlockDoor((Door)obj, indirect);
            }

            // TODO: test wrong preposition here
            throw new NotImplementedException("Unlock: wrong preposition");
        }

        private bool UnlockObject(Item obj)
        {
            if (!obj.IsLocked)
            {
                Print("It's unlocked at the moment.");
                return true;
            }

            if (obj.IsLockable)
            {
                // locking and unlock needs to be handled by the object in Before/After routines
                return true;
            }

            Print("That doesn't seem to be something you can unlock.");
            return true;
        }

        private bool UnlockDoor(Door door, Item indirect)
        {
            var key = door.Key;

            if (indirect == null)
            {
                if (key.InInventory)
                {
                    Print($"(with the {key.Name})");
                }
            }

            if (indirect != null && indirect != key)
            {
                Print("That doesn't seem to fit the lock.");
                return true;
            }

            if (key.InInventory)
            {
                Print($"You unlock the {door.Name}.");
                door.IsLocked = false;
                return true;
            }

            Print("You have nothing to unlock that with.");
            return true;
        }

        // TODO:"What do you want to unlock the {0} with?"
        //// this method basically shows a serious design flaw. because the parser does not
        //// currently remember the last command
        //private void ObjectNotSpecified()
        //{
        //    // do not use Print, go directly to output
        //    Output.Print("What do you want to unlock the {0} with?", Item.Name);

        //    string input = CommandPrompt.GetInput();
        //    if (string.IsNullOrEmpty(input))
        //    {
        //        Print(Messages.DoNotUnderstand);
        //        return;
        //    }

        //    var tokenizer = new InputTokenizer();
        //    var tokens = tokenizer.Tokenize(input);

        //    // player is answering the question (no verb specified) vs. re-typing
        //    // a complete sentence
        //    if (!tokens.StartsWithVerb())
        //    {
        //        // this is very simplistic right now
        //        input = "unlock " + Item.Synonyms[0] + " with " + String.Join(" ", tokens.ToArray());
        //    }

        //    Context.Parser.Parse(input);

        //}
    }
}
