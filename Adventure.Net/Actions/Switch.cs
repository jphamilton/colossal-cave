namespace Adventure.Net.Actions;

public class Switch : Verb
{
    public Switch()
    {
        Name = "switch";
    }

    public bool Expects(Object obj, Preposition.On on)
    {
        return Redirect<SwitchOn>(obj, v => v.Expects(obj, on));
    }

    public bool Expects(Object obj, Preposition.Off off)
    {
        return Redirect<SwitchOff>(obj, v => v.Expects(obj, off));
    }
}
