namespace Adventure.Net.ActionRoutines;

//Verb 'turn' 'rotate' 'screw' 'twist' 'unscrew'
//    * noun                                      -> Turn
//    * noun 'on'                                 -> Switchon
//    * noun 'off'                                -> Switchoff
//    * 'on' noun                                 -> Switchon
//    * 'off' noun                                -> Switchoff;

public class Turn : Routine
{
    public Turn()
    {
        Verbs = ["turn", "rotate", "screw", "twist", "unscrew"];
        Requires = [O.Noun];
    }
    public override bool Handler(Object obj, Object _ = null)
    {
        return Print("Nothing obvious happens.");
    }
}
