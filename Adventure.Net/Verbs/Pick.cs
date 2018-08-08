namespace Adventure.Net.Verbs
{
    public class Pick : Verb
    {
        public Pick()
        {
            Name = "pick up";
            Synonyms.Are("pick");
            Grammars.Add("up <multi>", PickUpObject);
            Grammars.Add("<multi> up", PickUpObject);
        }

        private bool PickUpObject()
        {
            return RedirectTo<Take>("<multi>");
        }

        
    }
}
