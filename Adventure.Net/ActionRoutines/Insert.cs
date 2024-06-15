namespace Adventure.Net.ActionRoutines;

/*
 Verb 'insert'
    * multiexcept 'in'/'into' noun -> Insert;
 */

public class Insert : Routine
{
    public Insert()
    {
        Verbs = ["insert"];
        Prepositions = ["in", "into"];
        Requires = [O.MultiExcept, O.Noun];
    }

    public override bool Handler(Object obj, Object second)
    {
        // receive
        var receive = second.Receive();

        if (receive != null)
        {
            return receive(obj);
        }

        if (second is Container container)
        {
            obj.Remove();
            container.Add(obj);
            return Context.Current.IsMulti ? Print("Done.") :
                Print($"You put {obj.DName} into the {container.Name}.");
        }

        return Fail("That can't contain things.");
    }
}

