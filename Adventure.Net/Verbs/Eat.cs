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

            if (obj.IsEdible)
            {
                // use Before/After routines on object to handle specific messages

                CurrentRoom.Objects.Remove(obj);
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
