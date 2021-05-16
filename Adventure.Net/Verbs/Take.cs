namespace Adventure.Net.Verbs
{
    public class Take : Verb
    {
        /*
        Verb 'take' 'carry' 'hold'
            * multi                                     -> Take
            * 'off' held                                -> Disrobe // not implemented
            * multiinside 'from'/'off' noun             -> Remove
            * 'inventory'                               -> Inv;    // not implemented
        */
        public Take()
        {
            Name = "take";
            Synonyms.Are("take", "carry", "hold");
            Grammars.Add("<multi>", TakeObject);
        }

        private bool TakeObject()
        {
            bool result = false;

            if (Item.IsScenery)
            {
                Print("That's hardly portable.");
            }
            else if (Item.IsStatic)
            {
                Print("That's fixed in place.");
            }
            else if (Inventory.Contains(Item))
            {
                Print("You already have that.");
            }
            else
            {
                Context.Story.Location.Objects.Remove(Item);
                Inventory.Add(Item);
                Print("Taken.");
                result = true;
            }

            return result;
        }

        public bool TakeObject(Item obj)
        {
            Item = obj;
            return TakeObject();
        }


    }
}
