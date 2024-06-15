namespace Adventure.Net.ActionRoutines;

public class OldMagic : Routine
{
    public OldMagic()
    {
        Verbs = ["sesame", "shazam", "hocus", "abracadabra", "foobar", "open-sesame", "frotz"];
    }

    public override bool Handler(Object _, Object __ = null)
    {
        return Print("Good try, but that is an old worn-out magic word.");
    }
}
