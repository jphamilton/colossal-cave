namespace Adventure.Net.Actions
{
    //Verb 'eat'
    //    * held                                      -> Eat;
    public class Eat : Verb
    {
        public Eat()
        {
            Name = "eat";
        }

        public bool Expects([Held] Item obj)
        {

            if (obj.IsEdible)
            {
                // use Before/After routines on object to handle specific messages
                Print($"You eat {obj}.");
                obj.Remove();
                Inventory.Remove(obj);
            }
            else
            {
                Print($"{obj.TheyreOrThats} plainly inedible.");
            }

            return true;
        }

    }
}
