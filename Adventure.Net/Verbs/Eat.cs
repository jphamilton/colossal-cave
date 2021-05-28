namespace Adventure.Net.Verbs
{

    // TODO: implement

    //Verb 'eat'
    //    * held                                      -> Eat;
    public class Eat : Verb
    {
        public Eat()
        {
            Name = "eat";
           // Grammars.Add("<held>", EatObject);
        }

        public bool Expects(Item obj)
        {
            if (!obj.IsEdible)
            {
                Print($"{obj.TheyreOrThats} plainly inedible.");
                return false;
            }
            
            //else if (obj.InInventory)
            //{
            //    Print($"You eat the {obj.Name}. Not bad.");
            //}

            //else
            //{
            //    CurrentRoom.Objects.Remove(obj);
            //    Inventory.Add(obj);
            //    Print($"(first taking the {obj.Name})");
            //}

            //Inventory.Remove(obj);

            return true;
        }

    }
}
