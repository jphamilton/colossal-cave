namespace Adventure.Net.Actions;

public class Insert : Verb
{
    public Insert()
    {
        Name = "insert";
        Multi = true;
        MultiHeld = true;
    }

    public bool Expects([Held]Object obj, Preposition.In @in, Object indirect)
    {
        return Receive(obj, indirect);
    }

    public bool Expects([Held] Object obj, Preposition.Into into, Object indirect)
    {
        return Receive(obj, indirect);
    }

    private bool Receive(Object obj, Object indirect)
    {
        // receive
        var receive = indirect.Receive();

        if (receive != null)
        {
            return receive(obj);
        }

        if (!Inventory.Contains(indirect))
        {
            return Print("You aren't holding that!");
        }

        if (indirect is Container container)
        {
            obj.Remove();
            container.Add(obj);
            return Context.Current.IsMulti ? Print("Done.") : 
                Print($"You put {obj.DefiniteArticle} {obj.Name} into the {container.Name}.");
        }

        return Print("That can't contain things.");
    }
}
