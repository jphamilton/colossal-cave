using System.Linq;

namespace Adventure.Net.Actions;

public class Disrobe : Verb
{
    public Disrobe()
    {
        Name = "disrobe";
        Synonyms.Are("doff", "shed");
    }

    public bool Expects()
    {
        var clothing = Inventory.Items.Where(x => x.Clothing && x.Worn).ToList();

        if (clothing.Count == 1)
        {
            var item = clothing[0];
            Print($"({item.DefiniteArticle} {item.Name})");
            return Expects(item);
        }

        return Print("What do you want to disrobe?");
    }

    public bool Expects([Held] Object obj)
    {
        if (obj.Clothing && obj.Worn)
        {
            obj.Worn = false;
            return Print($"You take off {obj.DefiniteArticle} {obj.Name}.");
        }

        return Print("You aren't wearing that!");
    }
}