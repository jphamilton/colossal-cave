namespace Adventure.Net.ActionRoutines;

/*
 Verb 'ask'
    * creature 'about' topic                    -> Ask
    * creature 'for' noun                       -> AskFor
    * creature 'to' topic                       -> AskTo
    * 'that' creature topic                     -> AskTo; -- NOT IMPLEMENTING
 */

public class Ask : Routine
{
    public Ask()
    {
        Verbs = ["ask"];
        Prepositions = ["about"];
        Requires = [O.Animate, O.Topic];
    }

    public override bool Handler(Object first, Object second = null)
    {
        return Print("There was no reply.");
    }
}

public class AskFor : Ask
{
    public AskFor()
    {
        Verbs = ["ask"];
        Prepositions = ["for"];
        Requires = [O.Animate, O.Noun];
    }
}

public class AskTo : Ask
{
    public AskTo()
    {
        Verbs = ["ask"];
        Prepositions = ["to"];
    }
}
