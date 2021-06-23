namespace Adventure.Net.Actions
{
    //Verb 'give' 'feed' 'offer' 'pay'
    //* creature held                             -> Give reverse
    //* held 'to' creature                        -> Give
    //* 'over' held 'to' creature                 -> Give; // will not support weird syntax no one will use
    public class Give : Verb
    {
        public Give()
        {
            Name = "give";
            Synonyms.Are("feed", "offer", "pay");
        }

        public bool Expects(Item creature, [Held]Item obj)
        {
            return GiveObject(obj, creature);
        }

        public bool Expects([Held]Item obj, Preposition.To to, Item creature)
        {
            return GiveObject(obj, creature);
        }

        private bool GiveObject(Item obj, Item creature)
        {
            if (creature.Animate)
            {
                Print($"The {creature} doesn't seem interested.");
            }
            else
            {
                Print("You can only do that with something animate.");
            }

            return true;
        }
    }
}
