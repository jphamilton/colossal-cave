using System.Linq;

namespace Adventure.Net.Actions;

public class Wear : Verb
{
    public Wear()
    {
        Name = "wear";
        Synonyms.Are("don");
    }

    public bool Expects()
    {
        // This works a little different than Inform 6. This will implicitly
        // wear clothing if inventory only contains 1 item that is wearable.
        var clothing = Inventory.Items.Where(x => x.Clothing).ToList();
        if (clothing.Count == 1)
        {
            var item = clothing[0];
            Print($"({item.DefiniteArticle} {item.Name})");
            return Expects(item);
        }

        return Print("What do you want to wear?");
    }

    public bool Expects([Held] Object obj)
    {
        if (!obj.Clothing)
        {
            return Print("You can't wear that!");
        }

        obj.Worn = true;

        return Print($"You put on {obj.DefiniteArticle} {obj.Name}.");
    }
}
