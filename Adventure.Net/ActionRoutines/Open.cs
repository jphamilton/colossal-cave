using Adventure.Net.Extensions;

namespace Adventure.Net.ActionRoutines;

//Verb 'open' 'uncover' 'undo' 'unwrap'
//    * noun                                      -> Open
//    * noun 'with' held                          -> Unlock

public class OpenWith : Unlock
{
    public OpenWith()
    {
        Verbs = ["open"];
        Prepositions = ["with"];
    }
}

public class Open : Routine
{
    public Open()
    {
        Verbs = ["open", "uncover", "undo", "unwrap"];
        Requires = [O.Noun];
        ImplicitMessage = (o) => $"(first opening {o.DName})";
    }

    public override bool Handler(Object obj, Object _ = null)
    {
        if (!obj.Openable)
        {
            return Fail($"{obj.TheyreOrThats} not something you can open.");
        }
        else if (obj.Locked)
        {
            string seems = obj.PluralName ? "seem" : "seems";
            return Fail($"{obj.DName} {seems} to be locked.");
        }
        else if (obj.Open)
        {
            return Fail($"{obj.TheyreOrThats} already open.");
        }
        else
        {
            obj.Open = true;

            if (obj is Container container && container.Children.Count > 0)
            {
                return Success($"You open {obj.DName}, revealing {obj.Children.DisplayList(false)}.");
            }
            
            return Print($"You open {obj.DName}.");
        }
    }
}
