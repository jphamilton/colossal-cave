using Adventure.Net.Things;
using System.Linq;

namespace Adventure.Net.ActionRoutines;

//Verb 'give' 'feed' 'offer' 'pay'
//* creature held                             -> Give reverse
//* held 'to' creature                        -> Give
//* 'over' held 'to' creature                 -> Give; // not implementing this syntax
public class Give : Routine
{
    public Give()
    {
        Verbs = ["give", "feed", "offer", "pay"];
        Requires = [O.Animate, O.Held]; // e.g. give bear food
        Reverse = true;
        ImplicitObject = (O r) => r == O.Animate ? Objects.Get<Player>() : Inventory.Count == 1 ? Inventory.Items[0] : null;
    }

    public override bool Handler(Object obj, Object creature) // parser will swap order
    {
        // Inform allows you to specify "reverse" - this happens so infrequently
        // it's just easier to check when you need to

        return HandleGive(obj, creature);
    }

    public static bool HandleGive(Object obj, Object indirect)
    {
        // Inform allows you to specify "reverse" - this happens so infrequently
        // it's just easier to check when you need to

        // Parser will swap the word order, but ultimately it's easier to check
        // because things like partial command responses are easier to handle

        var creature = obj.Animate ? obj : indirect.Animate ? indirect : null;
        var item = creature == obj ? indirect : obj;

        if (creature == null || !creature.Animate)
        {
            Print("You can only do that with something animate.");
            return true;
        }

        if (creature is Player)
        {
            Print($"You juggle {item.DName} for awhile, but don't achieve much.");
        }
        else if (creature.Animate)
        {
            Print($"The {creature} doesn't seem interested.");
        }

        return true;
    }
}

public class GiveTo : Give
{
    public GiveTo()
    {
        Verbs = ["give", "feed", "offer", "pay"];
        Prepositions = ["to"];
        Requires = [O.Held, O.Animate];
    }

    public override bool Handler(Object obj, Object indirect)
    {
        return HandleGive(obj, indirect);
    }
}
