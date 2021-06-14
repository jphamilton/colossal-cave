namespace Adventure.Net.Actions
{
    //Verb 'throw'
    //* held 'at'/'against'/'on'/'onto' noun      -> ThrowAt;
    public class Throw : Verb
    {
        public Throw()
        {
            Name = "throw";
        }

        public bool Expects([Held]Item obj, Preposition.At at, Item indirect)
        {
            return Redirect<ThrowAt>(obj, v => v.Expects(obj, at, indirect));
        }

        public bool Expects([Held] Item obj, Preposition.Against against, Item indirect)
        {
            return Redirect<ThrowAt>(obj, v => v.Expects(obj, new Preposition.At(), indirect));
        }

        public bool Expects([Held] Item obj, Preposition.On on, Item indirect)
        {
            return Redirect<ThrowAt>(obj, v => v.Expects(obj, new Preposition.At(), indirect));
        }

        public bool Expects([Held] Item obj, Preposition.Onto onto, Item indirect)
        {
            return Redirect<ThrowAt>(obj, v => v.Expects(obj, new Preposition.At(), indirect));
        }
    }
}
