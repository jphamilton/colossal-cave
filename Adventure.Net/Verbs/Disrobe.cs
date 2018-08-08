namespace Adventure.Net.Verbs
{
    public class Disrobe : Verb
    {
        public Disrobe()
        {
            Synonyms.Are("doff", "shed");
            Grammars.Add(K.HELD_TOKEN, OnDisrobe);
        }

        private bool OnDisrobe()
        {
            throw new System.NotImplementedException();
        }
    }
}