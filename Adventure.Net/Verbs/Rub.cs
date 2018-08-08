namespace Adventure.Net.Verbs
{
    public class Rub : Verb
    {
        public Rub()
        {
            Name = "rub";
            Grammars.Add("<noun>", RubObject);
        }

        private bool RubObject()
        {
            Print("You achieve nothing by this.");
            return true;
        }
    }
}
