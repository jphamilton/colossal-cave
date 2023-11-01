namespace Adventure.Net.Actions;

// TODO: implement
/*
     Verb 'insert'
        * multiexcept 'in'/'into' noun              -> Insert;
 */
public class Insert : Verb
{
    public Insert()
    {
        Name = "insert";
        Multi = true;
    }

    public bool Expects(Object obj, Preposition.In @in, Object indirect)
    {
        return Receive(obj, indirect);
    }

    public bool Expects(Object obj, Preposition.Into into, Object indirect)
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

        if (indirect is Container container)
        {
            obj.Remove();
            container.Add(obj);
            return Context.Current.IsMulti ? Print("Done.") : 
                Print($"You put {obj.IndefiniteArticle} {obj.Name} into the {container.Name}.");
        }

        return Print("That can't contain things.");
    }
}
