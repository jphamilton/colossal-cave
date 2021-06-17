namespace Adventure.Net.Actions
{
    public class Take : Verb
    {
        /*
        Verb 'take' 'carry' 'hold'
            * multi                                     -> Take
            * 'off' <held>                              -> Disrobe //TODO: not implemented
            * multiinside 'from'/'off' noun             -> Remove
            * 'inventory'                               -> Inv;    // TODO: not implemented
        */

        /*
         multi = means it handles an object list, all and except syntax
         held = must be in inventory
         */
        public Take()
        {
            Name = "take";
            Synonyms.Are("take", "carry", "hold");
            Multi = true;
        }

        public bool Expects(Item obj)
        {

            if (obj.IsScenery)
            {
                Print($"That's hardly portable.");
            }
            else if (obj.IsStatic)
            {
                Print($"That's fixed in place.");
            }
            else if (Inventory.Contains(obj))
            {
                Print($"You already have that.");
            }
            else
            {
                obj.Remove();
                Inventory.Add(obj);
                Print($"Taken.");
            }

            return true;
        }
      
        
    }
}
