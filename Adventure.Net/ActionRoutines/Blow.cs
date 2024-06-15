namespace Adventure.Net.ActionRoutines;

public class Blow : Routine
{
    public Blow()
    {
        Verbs = ["blow"];
        Requires = [O.Held];
    }

    public override bool Handler(Object first, Object second = null)
    {
        return Print($"You can't usefully blow {first.ThatOrThose}.");
    }
}
