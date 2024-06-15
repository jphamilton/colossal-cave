namespace Adventure.Net.ActionRoutines;

/*
 Verb 'read'
    * noun                                      -> Examine
    * 'about' topic 'in' noun                   -> Consult  // TODO: Not implemented
    * topic 'in' noun                           -> Consult; // TODO: Not implemented 
 */
public class Read : Examine
{
    public Read()
    {
        Verbs = ["read"];
    }
}