namespace Adventure.Net.Verbs
{
    /// <summary>
    /// Receive is a special verb that in only used for Before and After object routines.
    /// For example, "put batteries in lamp" will trigger the Before<Receive> routine
    /// for the lamp. This verb has no grammars defined and is used by the Insert verb.
    /// </summary>
    public class Receive : Verb
    {


    }
}
