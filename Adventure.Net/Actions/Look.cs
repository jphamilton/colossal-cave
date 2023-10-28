namespace Adventure.Net.Actions;

public class Look : Verb
{
    /*
     Verb 'look' 'l//'
        *                                           -> Look
        * 'at' noun                                 -> Examine
        * 'in'/'through'/'on' noun                  -> Search
        * 'under' noun                              -> LookUnder 
        * 'up' topic 'in' noun                      -> Consult
        * noun=ADirection                           -> Examine
        * 'to' noun=ADirection                      -> Examine;
     */
    public Look()
    {
        Name = "look";
        Synonyms.Are("l");
    }

    public bool Expects()
    {
        CurrentRoom.Look(true);
        return true;
    }

    public bool Expects(Preposition.Under under, Object obj)
    {
        return Redirect<LookUnder>(obj, v => v.Expects(obj));
    }

    public bool Expects(Object obj)
    {
        return Redirect<Examine>(obj, v => v.Expects(obj));
    }

    public bool Expects(Object obj, Preposition.At at)
    {
        return Redirect<Examine>(obj, v => v.Expects(obj));
    }
}
