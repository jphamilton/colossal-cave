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
        public Take()
        {
            Name = "take";
            Synonyms.Are("take", "carry", "hold");
            Multi = true;
        }

        public bool Expects(Object obj)
        {

            if (obj.Scenery)
            {
                Print($"That's hardly portable.");
            }
            else if (obj.Static)
            {
                Print($"That's fixed in place.");
            }
            else if (obj.Animate)
            {
                Print($"I don't suppose the {obj.Name} would care for that.");
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
