namespace Adventure.Net.ActionRoutines;

//Verb 'put'
//    * multiexcept 'in'/'inside'/'into' noun     -> Insert
//    * multiexcept 'on'/'onto' noun              -> PutOn
//    * 'on' held                                 -> Wear
//    * 'down' multiheld                          -> Drop
//    * multiheld 'down'                          -> Drop;

public class PutIn : Insert
{
    public PutIn()
    {
        Verbs = ["put"];
        Prepositions = ["in", "inside", "into"];
    }
}

public class PutOnTop : Routine
{
    public PutOnTop()
    {
        Verbs = ["put"];
        Prepositions = ["on", "onto"];
        Requires = [O.MultiExcept, O.Noun];
    }

    public override bool Handler(Object first, Object second)
    {
        if (second is Supporter supporter)
        {
            supporter.Add(first);
            return Context.Current.IsMulti ? Print("Done.") : Print($"You put {first.DName} on {supporter.DName}.");
        }

        return Fail($"Putting things on {second.DName} would achieve nothing.");
    }
}

public class PutOnClothing : Wear
{
    public PutOnClothing()
    {
        Verbs = ["put on"];
        Requires = [O.Held];
    }
}

public class PutDown : Drop
{
    public PutDown()
    {
        Verbs = ["put down"];
    }
}
