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

            if (Object.IsScenery)
            {
                Print("That's hardly portable.");
            }
            else if (Object.IsStatic)
            {
                Print("That's fixed in place.");
            }
            else if (Inventory.Contains(Object))
            {
                Print("You already have that.");
            }
            else
            {
                Context.Story.Location.Objects.Remove(Object);
                Inventory.Add(Object);
                Print("Taken.");
                result = true;
            }

            return result;
        }

        public bool TakeObject(Item obj)
        {
            Object = obj;
            return TakeObject();
        }


    }
}
