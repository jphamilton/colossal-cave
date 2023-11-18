namespace Adventure.Net.Actions;

public class Eat : Verb
{
    public Eat()
    {
        Name = "eat";
    }

    public bool Expects([Held] Object obj)
    {
        if (obj.Edible)
        {
            Print($"You eat {obj.DefiniteArticle} {obj.Name}.");
            obj.Remove();
        }
        else
        {
            Print($"{obj.TheyreOrThats} plainly inedible.");
        }

        return true;
    }

}
