namespace Adventure.Net.Actions;

public class Unlock : Verb
{
    public Unlock()
    {
        Name = "unlock";
    }

    public bool Expects(Object obj, Preposition.With with, [Held] Object indirect)
    {
        return UnlockObject(obj, indirect);
    }

    private bool UnlockObject(Object obj, Object indirect)
    {
        var key = obj.Key;

        if (indirect != null && indirect != key)
        {
            Print($"The {indirect.Name} doesn't seem to fit the lock.");
            return true;
        }

        if (key != null && key.InInventory)
        {
            if (obj.Locked)
            {
                Print($"You unlock the {obj.Name}.");
                obj.Locked = false;
            }
            else
            {
                Print("That's unlocked at the moment.");
            }

            return true;
        }

        Print("You have nothing to unlock that with.");
        return true;
    }

}
