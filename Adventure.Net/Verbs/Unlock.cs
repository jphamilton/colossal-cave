namespace Adventure.Net.Verbs
{
    // TODO: implement
    public class Unlock : Verb
    {
        public Unlock()
        {
            Name = "unlock";
        }

       
        // implicit unlock
        // if the key is in inventory and is the only item, the
        public bool Expects(Item obj)
        {
            return UnlockObject(obj);
            //if (!obj.IsLockable)
            //{
            //    Print("That doesn't seem to be something you can unlock.");
            //}
            //else if (!obj.IsLocked)
            //{
            //    Print("It's unlocked at the moment.");
            //}



        }

        public bool Expects(Item obj, Preposition.With with, [Held]Item indirect)
        {
            return UnlockObject(obj, indirect);
        }

        private bool UnlockObject(Item obj, Item indirect = null)
        {
            var key = obj.Key;

            if (indirect == null)
            {
                // implicit unlock
                if (key != null && key.InInventory && Inventory.Count == 1)
                {
                    Print($"(with the {key.Name})");
                }
                else
                {
                    Print($"What do you want to unlock {obj.Article} {obj.Name} with?");
                    return true;
                }
            }

            if (indirect != null && indirect != key)
            {
                Print("That doesn't seem to fit the lock.");
                return true;
            }

            if (key != null && key.InInventory)
            {
                if (obj.IsLocked)
                {
                    Print($"You unlock the {obj.Name}.");
                    obj.IsLocked = false;
                }
                else
                {
                    Print("That's unlocked at the moment.");
                }

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
