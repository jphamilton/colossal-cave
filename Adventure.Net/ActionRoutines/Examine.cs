using Adventure.Net.Extensions;
using Adventure.Net.Things;

namespace Adventure.Net.ActionRoutines;

public class Examine : Routine
{
    public Examine()
    {
        Verbs = ["examine", "x", "check", "describe", "watch"];
        Requires = [O.Noun];
    }

    public override bool Handler(Object obj, Object _)
    {
        if (!CurrentRoom.IsLit())
        {
            return Print("Darkness, noun. An absence of light to see by.");
        }

        if (obj is Player)
        {
            return Print("As good-looking as ever.");
        }
        else if (obj.Scenery && string.IsNullOrEmpty(obj.Description))
        {
            return Print($"You see nothing special about {obj.DName}.");
        }
        else if (obj is Room)
        {
            return Print("That's not something you need to refer to in the course of this game.");
        }
        else if (obj.Describe != null)
        {
            string describe = obj.Describe();
            return describe.HasValue() ? Print(describe) : Print(obj.Description);
        }
        else if (obj.Description.HasValue())
        {
            return Print(obj.Description);
        }
        else
        {
            return Print("That's not something you need to refer to in the course of this game.");
        }
    }
}
