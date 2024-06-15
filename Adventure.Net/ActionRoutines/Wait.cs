namespace Adventure.Net.ActionRoutines;

public class Wait : Routine
{
    public Wait()
    {
        Verbs = ["wait"];
    }
    public override bool Handler(Object _, Object __)
    {
        return Print("Time passes.");
    }
}
