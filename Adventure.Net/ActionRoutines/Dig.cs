namespace Adventure.Net.ActionRoutines;

//Verb 'dig'
//    * noun                                      -> Dig
//    * noun 'with' held                          -> Dig
//    * 'in' noun                                 -> Dig
//    * 'in' noun 'with' held                     -> Dig; // not supporting this syntax
public class Dig : Routine
{
    
    public Dig()
    {
        Verbs = ["dig"];
        Requires = [O.Noun];
    }

    public override bool Handler(Object first, Object second = null)
    {
        return Print($"Digging would achieve nothing here.");
    }
}

public class DigWith : Dig
{

    public DigWith()
    {
        Verbs = ["dig"];
        Prepositions = ["with"];
        Requires = [O.Noun, O.Held];
    }
}

public class DigIn : Dig
{

    public DigIn()
    {
        Verbs = ["dig"];
        Prepositions = ["in"];
        Requires = [O.Noun];
    }
}
