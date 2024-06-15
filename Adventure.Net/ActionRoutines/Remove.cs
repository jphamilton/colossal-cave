
/*
Verb 'remove'
* held                                      -> Disrobe
* multi                                     -> Take (NOT IMPLEMENTING)

* multiinside 'from' noun                   -> Remove; 
* multiinside 'from' noun                   -> // ALSO TAKE??? 

* multiinside 'off' noun                    -> not implemented

*/

namespace Adventure.Net.ActionRoutines;

public class Remove : Disrobe
{
    public Remove()
    {
        Verbs = ["remove"];
        Requires = [O.Held];
    }
}

public class RemoveFrom : Take //Routine
{
    public RemoveFrom()
    {
        Verbs = ["remove"];
        Prepositions = ["from"];
        Requires = [O.MultiInside, O.Noun];
    }

    public override bool Handler(Object first, Object second)
    {
        if (second is not Container)
        {
            return Print(Messages.CantSeeObject);
        }

        if (second is Container container && container.Contains(first))
        {
            if (!container.Open && !Implicit.Action<Open>(second))
            {
                return false;
            }

            // subtle difference here
            // if container is in inventory, the object inside has essensitally been taken already
            if (Inventory.Contains(container))
            {
                Inventory.Add(first);
                return Print("Removed.");
            }
            else if (Inventory.CanAdd())
            {
                Inventory.Add(first);
                return Print("Removed.");
            }
            else
            {
                return Fail(Messages.CarryingTooMuch);
            }
        }

        return Fail(Messages.CantSeeObject);
    }
}
