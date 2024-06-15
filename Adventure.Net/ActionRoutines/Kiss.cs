namespace Adventure.Net.ActionRoutines;

public class Kiss : Routine
{
    public Kiss()
    {
        Verbs = ["kiss", "embrace", "hug"];
        Requires = [O.Animate];
    }
    public override bool Handler(Object first, Object second = null)
    {
        return Print("Keep your mind on the game.");
    }
}
