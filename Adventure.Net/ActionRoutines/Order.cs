namespace Adventure.Net.ActionRoutines;

public class Order : Routine
{
    public Order()
    {
        Verbs = ["order"];
        Requires = [O.Animate];
    }

    public override bool Handler(Object creature, Object _)
    {
        return Fail($"{creature.DName} has better things to do.");
    }
}
