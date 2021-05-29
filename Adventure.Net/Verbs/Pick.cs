namespace Adventure.Net.Verbs
{
    //Verb 'pick'
    //    * 'up' multi                                -> Take
    //    * multi 'up'                                -> Take;
    public class Pick : Verb
    {
        public Pick()
        {
            Name = "pick up";
            Synonyms.Are("pick");
            Multi = true;
        }

        public bool Expects(Item obj, Up up)
        {
            return Redirect<Take>(obj, v => v.Expects(obj));
        }

    }
}
