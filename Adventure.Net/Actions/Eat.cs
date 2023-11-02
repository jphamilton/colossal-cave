namespace Adventure.Net.Actions;

//Verb 'eat'
//    * held                                      -> Eat;
public class Eat : Verb
{
    public Eat()
    {
        Name = "eat";
        Held = true;
    }

    public bool Expects(Object obj)
    {

        if (obj.Edible)
        {
            // use Before/After routines on object to handle specific messages
            Print($"You eat {obj}.");
            obj.Remove();
        }
        else
        {
            Print($"{obj.TheyreOrThats} plainly inedible.");
        }

        return true;
    }

}
