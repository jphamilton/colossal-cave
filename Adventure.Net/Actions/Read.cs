namespace Adventure.Net.Actions;

/*
 Verb 'read'
    * noun                                      -> Examine
    * 'about' topic 'in' noun                   -> Consult  // TODO: Not implemented
    * topic 'in' noun                           -> Consult; // TODO: Not implemented 
 */
public class Read : Verb
{
    public Read()
    {
        Name = "read";
    }

    public bool Expects(Object obj)
    {
        return Redirect<Examine>(o => o.Expects(obj));
    }
}
