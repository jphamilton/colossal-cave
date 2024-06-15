namespace Adventure.Net.ActionRoutines;

// for testing purposes
public class Death : Routine
{
    public Death()
    {
        Verbs = ["avada kedavra"];
    }

    public override bool Handler(Object first, Object second = null) => throw new DeathException();
}
