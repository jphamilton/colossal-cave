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

        public bool Expects([Held]Object obj, Preposition.At at, Object indirect)
        {
            return Redirect<ThrowAt>(obj, v => v.Expects(obj, at, indirect));
        }

        public bool Expects([Held] Object obj, Preposition.Against against, Object indirect)
        {
            return Redirect<ThrowAt>(obj, v => v.Expects(obj, new Preposition.At(), indirect));
        }

        public bool Expects([Held] Object obj, Preposition.On on, Object indirect)
        {
            return Redirect<ThrowAt>(obj, v => v.Expects(obj, new Preposition.At(), indirect));
        }

        public bool Expects([Held] Object obj, Preposition.Onto onto, Object indirect)
        {
            return Redirect<ThrowAt>(obj, v => v.Expects(obj, new Preposition.At(), indirect));
        }
    }
}
