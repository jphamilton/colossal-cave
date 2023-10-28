using System;

namespace Adventure.Net.Actions;

//Verb 'empty'
//    * noun                                      -> Empty
//    * 'out' noun                                -> Empty
//    * noun 'out'                                -> Empty
//    * noun 'to'/'into'/'on'/'onto' noun         -> EmptyT;
public class Empty : Verb
{
    public Empty()
    {
        Name = "empty";
    }

    public bool Expects(Object obj)
    {
        return EmptyObject(obj);
    }

    public bool Expects(Object obj, Preposition.Out @out)
    {
        return EmptyObject(obj);
    }

    public bool Expects(Object obj, Preposition.To to, Object indirect)
    {
        return Pour(obj, indirect);
    }

    public bool Expects(Object obj, Preposition.Into into, Object indirect)
    {
        return Pour(obj, indirect);
    }

    public bool Expects(Object obj, Preposition.On on, Object indirect)
    {
        return Pour(obj, indirect);
    }
    public bool Expects(Object obj, Preposition.Onto onto, Object indirect)
    {
        return Pour(obj, indirect);
    }

    private bool EmptyObject(Object obj)
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

        return true;
    }

    private bool Pour(Object obj, Object indirect)
    {
        throw new NotImplementedException();
    }
}

