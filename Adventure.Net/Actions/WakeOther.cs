namespace Adventure.Net.Actions;

public class WakeOther : Verb
{
    public bool Expects(Object creature)
    {
        return Print("That seems unnecessary.");
    }
}
