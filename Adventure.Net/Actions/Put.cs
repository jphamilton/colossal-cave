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
    public bool Expects([Held] Object obj, Preposition.In @in, Object indirect)
    {
        if (!Inventory.Contains(indirect))
        {
            Print("You aren't holding that!");
            return false;
        }

        return Redirect<Insert>(obj, v => v.Expects(obj, @in, indirect));
    }

    // put something on top of something else
    public bool Expects([Held] Object obj, Preposition.On on, Object indirect)
    {
        if (indirect is Supporter supporter)
        {
            supporter.Add(obj);
            return Context.Current.IsMulti ? Print("Done.") : Print($"You put {obj.DefiniteArticle} {obj.Name} on {supporter.DefiniteArticle} {supporter.Name}.");
        }

        return Print($"Putting things on {indirect.DefiniteArticle} {indirect.Name} would achieve nothing.");
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

}
