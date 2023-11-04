namespace Adventure.Net.Actions;

//public class TurnOff : Verb
//{
//    public TurnOff()
//    {
//        Name = "turn off";
//    }

//    public bool Expects(Object obj)
//    {
//        return Redirect<SwitchOff>(obj, v => v.Expects(obj, new Preposition.Off()));
//    }
//}

public class Turn : Verb
{
    //Verb 'turn' 'rotate' 'screw' 'twist' 'unscrew'
    //    * noun                                      -> Turn
    //    * noun 'on'                                 -> Switchon
    //    * noun 'off'                                -> Switchoff
    //    * 'on' noun                                 -> Switchon
    //    * 'off' noun                                -> Switchoff;

    public Turn()
    {
        Name = "turn";
        Synonyms.Are("turn", "rotate", "screw", "twist", "unscrew");
    }

    public bool Expects(Object obj, Preposition.On on)
    {
        return Redirect<Switch>(obj, v => v.Expects(obj, on));
    }

    public bool Expects(Object obj, Preposition.Off off)
    {
        return Redirect<Switch>(obj, v => v.Expects(obj, off));
    }

    // turn dial, knob, etc.
    public bool Expects(Object item)
    {
        // must implemented using Before/After
        Print("Nothing obvious happens.");
        return true;
    }

}
