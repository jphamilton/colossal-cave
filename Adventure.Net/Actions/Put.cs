using System;

namespace Adventure.Net.Actions;

// TODO: implement

//Verb 'put'
//    * multiexcept 'in'/'inside'/'into' noun     -> Insert
//    * multiexcept 'on'/'onto' noun              -> PutOn
//    * 'on' held                                 -> Wear
//    * 'down' multiheld                          -> Drop
//    * multiheld 'down'                          -> Drop;
public class Put : Verb
{
    public Put()
    {
        Name = "put";
        Multi = true;
        MultiHeld = true;
    }

    // insert
    public bool Expects(Object obj, Preposition.In @in, Object indirect)
    {
        if (!Inventory.Contains(indirect))
        {
            Print("You aren't holding that!");
            return false;
        }

        return Redirect<Insert>(obj, v => v.Expects(obj, @in, indirect));
    }

    // put something on top of something else
    public bool Expects(Object obj, Preposition.On on, Object indirect)
    {
        return PutOnTop(obj, indirect);
    }

    // put object down
    public bool Expects(Object obj, Preposition.Down down)
    {
        return Redirect<Drop>(obj, v => v.Expects(obj));
    }

    // wear
    public bool Expects(Object obj, Preposition.On on)
    {
        if (!obj.Wearable)
        {
            Print($"What do you want to put {obj.DefiniteArticle} {obj.Name} on?");
            return false;
        }

        return Redirect<Wear>(obj, v => v.Expects(obj));
    }

    private bool PutOnTop(Object obj, Object indirect)
    {
        if (!obj.InInventory)
        {
            Print($"You need to be holding the {obj.Name} before you can put it on top of something else.");
            return true;
        }

        throw new NotImplementedException("Put (on) not implemented");
    }


}
