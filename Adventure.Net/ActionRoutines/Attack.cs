namespace Adventure.Net.ActionRoutines;

public class Attack : Routine
{
    public Attack()
    {
        Verbs = ["attack", "break", "crack", "destroy", "fight", "hit", "kill", "murder", "punch", "smash", "thump", "torture", "wreck"];
        Requires = [O.Noun];
    }

    public override bool Handler(Object first, Object second = null)
    {
        return Print("Violence isn't the answer to this one.");
    }
}