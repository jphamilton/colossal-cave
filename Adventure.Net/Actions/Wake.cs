namespace Adventure.Net.Actions
{
    public class Wake : Verb
    {
        public Wake()
        {
            Name = "wake";
            Synonyms.Are("awake", "awaken");
        }

        public bool Expects()
        {
            return WakeUp();
        }

        public bool Expects(Preposition.Up up)
        {
            return WakeUp();
        }

        public bool Expects(Object obj)
        {
            return WakeOther(obj);
        }

        public bool Expects(Object obj, Preposition.Up up)
        {
            return WakeOther(obj);
        }

        private bool WakeUp()
        {
            return Print("The dreadful truth is, this is not a dream.");
        }

        private bool WakeOther(Object obj)
        {
            if (!obj.Animate)
            {
                return Print("You can only do that to something animate.");
            }

            return Redirect<WakeOther>(obj, v => v.Expects(obj));
        }
    }
}

