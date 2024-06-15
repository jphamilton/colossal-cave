using Adventure.Net.Extensions;
using System.Linq;

namespace Adventure.Net.ActionRoutines;

public class Empty : Routine
{
    public Empty()
    {
        Verbs = ["empty"];
        Requires = [O.Noun];
    }

    public override bool Handler(Object obj, Object _)
    {
        Container container = obj as Container;

        if (container == null)
        {
            Print($"The {obj.Name} can't contain things.");
        }
        else if (!container.Open)
        {
            Print($"The {container.Name} {container.IsOrAre} closed.");
        }
        else if (container.IsEmpty)
        {
            Print($"The {container.Name} {container.IsOrAre} empty already.");
        }
        else
        {
            Print("That would scarcely empty anything.");
        }

        return false;
    }
}

public class EmptyOut : Empty
{
    public EmptyOut()
    {
        Prepositions = ["out"];
    }
}

public class EmptyOnto : Routine
{
    public EmptyOnto()
    {
        Verbs = ["empty"];
        Prepositions = ["to", "in", "into", "on", "onto"];
        Requires = [O.Noun, O.Noun];
    }

    public override bool Handler(Object first, Object second)
    {
        if (first is not Container)
        {
            return Fail($"{first.DName.Capitalize()} can't contain things.");
        }
        if (second is not Container && second is not Supporter)
        {
            return Fail($"{second.DName.Capitalize()} can't contain things.");
        }
        else if (first is Container container)
        {
            if (container.Children.Count == 0)
            {
                return Fail($"{container.DName.Capitalize()} is empty already.");
            }

            if (second == container)
            {
                return Fail("That wouldn't empty anything.");
            }

            if (!container.Open)
            {
                if (!Implicit.Action<Open>(container))
                {
                    return false;
                }
            }

            var result = true;

            foreach (var child in container.Children.ToList())
            {
                using var _ = Output.Override(x => $"{child.Name}: {x}");
                if (second is Container && !Redirect.To<Insert>(child, second))
                {
                    result = false;
                }
                else if (second is Supporter && Redirect.To<PutOnTop>(child, second))
                {
                    result = false;
                }
            }

            return result;
        }

        return true;
    }
}