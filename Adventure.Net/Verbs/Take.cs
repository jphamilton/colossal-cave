using System.Collections.Generic;

namespace Adventure.Net.Verbs
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
            // TODO: do we really even need to know that something is "Multi"?
            Multi = true;
           // Grammars.Add("<multi>", TakeObject);
        }

        /* TODO: need to handle multi output response with messages from before/after routines
           brass lantern: Taken.
           keys: Taken.
           carrot: It's glued to the floor!
           small bottle: Taken.
         */

        public bool Expects(Item obj)
        {
            
            return TakeObject(obj);
        }

        public bool Expects(IList<Item> objects)
        {
            // TODO: implement Take multi
            return false;
        }

        // TODO: Finish Take implementation
        private bool TakeObject(Item obj, bool fromMulti = false)
        {
            bool result = false;
            string prefix = fromMulti ? $"{obj.Name}: " : "";

            if (obj.IsScenery)
            {
                Print($"{prefix}That's hardly portable.");
            }
            else if (obj.IsStatic)
            {
                Print($"{prefix}That's fixed in place.");
            }
            else if (Inventory.Contains(obj))
            {
                Print($"{prefix}You already have that.");
            }
            else
            {
                CurrentRoom.Objects.Remove(obj);
                Inventory.Add(obj);
                Print($"{prefix}Taken.");
                result = true;
            }

            return result;
        }

    }
}
