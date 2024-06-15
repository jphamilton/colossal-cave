namespace Adventure.Net.ActionRoutines;

//Verb 'throw'
//    * held 'at'/'against'/'on'/'onto' noun      -> ThrowAt;
public class Throw : Routine
{
    public Throw()
    {
        Verbs = ["throw"];
        Prepositions = ["at", "against", "on", "onto"];
        Requires = [O.Held, O.Noun];
    }

    public override bool Handler(Object held, Object noun)
    {
        if (noun.Animate)
        {
            return Fail("You lack the nerve when it comes to the crucial moment.");
        }
        
        return Fail("Futile.");
    }

}
