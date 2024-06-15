namespace Adventure.Net.ActionRoutines;

//Verb 'pull' 'drag'
//    * noun                                      -> Pull;

public class Push : Routine
{
    public Push()
    {
        Verbs = ["push", "clear", "move", "press", "shift"];
        Requires = [O.Noun];
    }

    public override bool Handler(Object obj, Object _)
    {
        if (obj.Scenery || obj.Static)
        {
            return Fail("That is fixed in place.");
        }
        
        return Fail("Nothing obvious happens.");
    }
}

public class Pull : Push
{
    public Pull()
    {
        Verbs = ["pull", "drag"];
    }
}
