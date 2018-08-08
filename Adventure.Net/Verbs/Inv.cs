namespace Adventure.Net.Verbs
{
    public class Inv : Verb
    {
        public Inv()
        {
            Name = "inventory";
            Synonyms.Are("i", "inv", "inventory");
            Grammars.Add(Grammar.Empty, DisplayInventory);
        }

        public bool DisplayInventory()
        {
            Print(Inventory.Display());
            return true;
        }
    }
}
