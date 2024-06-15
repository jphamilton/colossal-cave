namespace Adventure.Net.ActionRoutines;

/// <summary>
/// Simply redirects to routine of T
/// </summary>
/// <remarks>
/// Success messages are suppressed which allows (first opening the wicker cage)
/// to be printed without the "You open the wicker cage" response
/// </remarks>
public static class Implicit
{
    public static bool Action<T>(Object first, Object second = null) where T : Routine
    {
        var routine = Routines.Get<T>();

        if (routine.GetType() == typeof(Take))
        {
            return Action<ImplicitTake>(first, second);
        }

        bool success = false;

        if (routine.ImplicitMessage != null)
        {
            Output.Print(routine.ImplicitMessage(first));
        }

        if (Redirect.To<T>(first, second))
        {
            // remove any messages that were printed
            Context.Current.OrderedOutput = Context.Current.OrderedOutput[..^1];
            success = true;
        }

        return success;
    }
}
