using Adventure.Net.Extensions;

namespace Adventure.Net.ActionRoutines;

//Verb 'close' 'cover' 'shut'
//    * noun                                      -> Close
//    * 'up' noun                                 -> Close
//    * 'off' noun                                -> SwitchOff;

public class Close : Routine
{
    public Close()
    {
        Verbs = ["close", "cover", "shut"];
        Requires = [O.Noun];
    }

    public override bool Handler(Object obj, Object _)
    {
        if (!obj.Openable)
        {
            Print($"{obj.TheyreOrThats.Capitalize()} not something you can close.");
        }
        else if (!obj.Open)
        {
            Print($"{obj.TheyreOrThats.Capitalize()} already closed.");
        }
        else
        {
            obj.Open = false;
            Print($"You close {obj.DName}.");
        }

        return true;
    }
}

public class CloseUp : Close
{
    public CloseUp()
    {
        Verbs = ["close up"];
    }
}

public class CloseOff : SwitchOff
{
    public CloseOff()
    {
        Verbs = ["close off"];
    }
}