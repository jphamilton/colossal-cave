using Adventure.Net.Extensions;
using Adventure.Net.Things;
using System.Linq;

namespace Adventure.Net.ActionRoutines;

/*
    Verb 'take' 'carry' 'hold'
        * multi                                     -> Take
        * 'off' <held>                              -> Disrobe
        * multiinside 'from'/'off' noun             -> Remove  
    */


public class ImplicitTake : Take
{
    public ImplicitTake()
    {
        Verbs = [];
        Requires = [];
        ImplicitMessage = (o) =>
        {
            // container is openable, not locked, touched etc, etc. - lots to consider here that we are not
            // Parser should determine if obj is accessible before making it this far
            if (o.Parent is Container container)
            {
                return $"(first taking {o.DName} out of {container.DName})";
            }
            else if (o.Parent is Supporter supporter)
            {
                return $"(first taking {o.DName} off of {supporter.DName})";
            }

            return $"(first taking {o.DName})";
        };
    }

    public override bool Handler(Object first, Object _ = null)
    {
        Context.Current.PushState();

        base.Handler(first, _);

        Context.Current.PopState();

        // clear output
        Context.Current.OrderedOutput = [];

        return Inventory.Contains(first);
    }
}

public class Take : Routine
{
    public Take()
    {
        Verbs = ["take", "carry", "hold"];
        Requires = [O.Multi];
        ImplicitMessage = (o) =>
        {
            // container is openable, not locked, touched etc, etc. - lots to consider here that we are not
            // Parser should determine if obj is accessible before making it this far
            if (o.Parent is Container container)
            {
                return $"(first taking {o.DName} out of {container.DName})";
            }
            else if (o.Parent is Supporter supporter)
            {
                return $"(first taking {o.DName} off of {supporter.DName})";
            }

            return $"(first taking {o.DName})";
        };


        ImplicitObject = (_) =>
        {
            var inScope = CurrentRoom.ObjectsInScope();
            inScope = [.. inScope.Where(x => !Inventory.Contains(x))];

            var available = (from x in inScope
                             where !x.Scenery && !x.Static && !x.Animate && !x.Absent
                             select x).ToList();

            if (available.Count == 1)
            {
                return available[0];
            }

            return null;
        };
    }

    public override bool Handler(Object first, Object second = null)
    {
        if (first is Player)
        {
            return Fail("You're always self-possessed.");
        }
        if (first.Scenery)
        {
            return Fail($"{first.DName.Capitalize()} is hardly portable.");
        }
        else if (first.Static)
        {
            return Fail($"{first.DName.Capitalize()} is fixed in place.");
        }
        else if (first.Animate)
        {
            return Fail($"I don't suppose {first.DName} would care for that.");
        }
        else if (Inventory.Contains(first))
        {
            return Fail("You already have that.");
        }
        else if (Inventory.CanAdd())
        {
            Inventory.Add(first);
            return Success("Taken.");
        }
        else
        {
            return Fail(Messages.CarryingTooMuch);
        }
    }
}

public class TakeOff : Disrobe
{
    public TakeOff()
    {
        Verbs = ["take off"];
    }
}

public class TakeFrom : RemoveFrom
{
    // collision with TakeOff maybe -- this has obj prep obj structure
    public TakeFrom()
    {
        Verbs = ["take", "carry", "hold"];
        Prepositions = ["from", "off"]; // <-- off is collision, need to distinguish routines that accept indirect objects vs. just objects
        Requires = [O.MultiInside, O.Noun];
    }
}

public class PickUp : Take
{
    public PickUp()
    {
        Verbs = ["pick up"];
    }
}
