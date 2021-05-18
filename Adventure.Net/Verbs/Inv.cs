namespace Adventure.Net.Verbs
{
    // TODO: implement
    public class Inv : Verb
    {
        public Inv()
        {
            Name = "inventory";
            Synonyms.Are("i", "inv", "inventory");
           // Grammars.Add(Grammar.Empty, DisplayInventory);
        }

        //public bool DisplayInventory()
        //{
        //    Print(Inventory.Display());
        //    return true;
        //}
    }
}
