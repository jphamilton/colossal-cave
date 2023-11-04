namespace Adventure.Net.Actions;

public class Wear : Verb
{
    public Wear()
    {
        Name = "wear";
        Synonyms.Are("don");
    }

    public bool Expects([Held] Object obj)
    {
        if (!obj.Clothing)
        {
            return Print("You can't wear that!");
        }

        obj.Worn = true;
        Inventory.Add(obj);
        return Print($"You put on {obj.DefiniteArticle} {obj.Name}.");
    }
}
