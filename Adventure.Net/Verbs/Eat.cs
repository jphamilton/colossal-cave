namespace Adventure.Net.Verbs
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
            // use Before/After routines on object to handle specific implementations

            if (!obj.IsEdible)
            {
                Print($"{obj.TheyreOrThats} plainly inedible.");
                return false;
            }

            CurrentRoom.Objects.Remove(obj);
            Inventory.Remove(obj);

            return true;
        }

    }
}
