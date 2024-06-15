namespace Adventure.Net.ActionRoutines;

/*
    Verb 'go' 'run' 'walk'
    *                                           -> VagueGo
    * noun=ADirection                           -> Go
    * noun                                      -> Enter
    * 'out'                                     -> Exit     
    * 'in'                                      -> GoIn    WHY is this different from Enter?
    * 'in'/'through' noun                       -> Enter;
*/
public class VagueGo : Routine
{
    public VagueGo()
    {
        Verbs = ["go", "walk", "run"];
        Requires = [];
    }

    public override bool Handler(Object _, Object __)
    {
        return Fail("You'll have to say which compass direction to go in.");
    }
}

public class Go : Enter
{
    public Go()
    {
        Verbs = ["go", "walk", "run"];
    }

    public override bool Handler(Object _, Object __)
    {
        return Fail("You'll have to say which compass direction to go in.");
    }
}

public class GoIn : Enter
{
    public GoIn()
    {
        Verbs = ["go", "walk", "run"];
        Prepositions = ["in", "through"];
    }
}

public class GoOut : Exit
{
    public GoOut()
    {
        Verbs = ["go", "walk", "run"];
        Prepositions = ["out"];
    }
}
