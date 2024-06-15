namespace Adventure.Net.ActionRoutines;

// TODO: implement
// Verb 'jump' 'hop' 'skip'
//*                                           -> Jump
//* 'in' noun                                 -> JumpIn
//* 'into' noun                               -> JumpIn
//* 'on' noun                                 -> JumpOn
//* 'upon' noun                               -> JumpOn
//* 'over' noun                               -> JumpOver;

public class Jump : Routine
{
    public Jump()
    {
        Verbs = ["jump", "hop", "skip"];
    }
    
    public override bool Handler(Object obj, Object _)
    {
        Print("You jump on the spot, fruitlessly.");
        return true;
    }
}

public class JumpIn : Jump
{
    public JumpIn()
    {
        Prepositions = ["in", "into"];
        Requires = [O.Noun];
    }

    public override bool Handler(Object obj, Object _)
    {
        return Fail($"Jumping in {obj.DName} would achieve nothing here.");
    }
}

public class JumpOn : Jump
{
    public JumpOn()
    {
        Prepositions = ["on", "upon"];
        Requires = [O.Noun];
    }

    public override bool Handler(Object obj, Object _)
    {
        return Fail($"Jumping on {obj.DName} would achieve nothing here.");
    }
}

public class JumpOver : Jump
{
    public JumpOver()
    {
        Prepositions = ["over"];
        Requires = [O.Noun];
    }

    public override bool Handler(Object obj, Object _)
    {
        return Fail($"Jumping over {obj.DName} would achieve nothing here.");
    }
}