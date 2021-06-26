namespace Adventure.Net.Actions
{
    public class Disrobe : Verb
    {
    //    Verb 'disrobe' 'doff' 'shed'
    //*   held                                      -> Disrobe;
        public Disrobe()
        {
            Name = "disrobe";
            Synonyms.Are("doff", "shed");
        }

        public bool Expects([Held]Object obj)
        {
            return Print("this is not implemented");
        }
    }
}