using Adventure.Net.Extensions;
using Adventure.Net.Things;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Adventure.Net.ActionRoutines;

/*
noun - any object in scope

held - object held by the actor - held, noun 'with' held, 'prep' held, held 'prep', held 'prep' noun

multi - one or more objects in scope - mostly just multi, but can be multi 'prep' or 'prep' multi

multiheld - one or more held objects - can be: 'prep' multiheld, multiheld 'prep', or just multiheld

multiexcept - one or more in scope, except the other object - always : multiexcept 'prep' noun

multiinside - one or more in scope, inside the other object - always : multiinside 'prep' noun

NEED TO HANDLE
topic - ADD TO PARSER - handle animate check - topic 'to' creature, say go away now to <creature>

NOT HANDLING
hattributei any object in scope which has the attribute
creature - an object in scope which is animate
noun -  hRoutinei any object in scope passing the given test
scope -  hRoutinei an object in this definition of scope
number - a number only
hRoutinei - any text accepted by the given routine
 */

public enum O
{
    None = 0,
    Noun,
    Held,
    Multi,
    MultiHeld,
    MultiInside,
    MultiExcept,
    Animate,
    Topic
}

[DebuggerDisplay("{DebuggerDisplay,nq}")]
public abstract class Routine
{
    private List<O> _multiTypes = [O.Multi, O.MultiHeld, O.MultiExcept, O.MultiInside];
    private List<O> _requires = [];

    public List<string> Verbs { get; protected set; } = [];
    
    public List<string> Prepositions { get; protected set; } = [];
    
    public List<O> Requires
    {
        get => _requires;
        protected set
        {
            if (value == null)
            {
                throw new Exception("Requires cannot be null");
            }

            if (value.Contains(O.None))
            {
                throw new Exception($"[{GetType().Name}]: Object type O.None is reserved");
            }

            if (value.Count > 1)
            {
                var type = value[1];

                if (_multiTypes.Contains(type))
                {
                    throw new Exception($"[{GetType().Name}]: Indirect objects cannot be {type}");
                }
            }

            _requires = value;
        }
    }

    /// <summary>
    /// Handler for action routine (verb)
    /// </summary>
    /// <returns>Should return <c>true</c> on success.
    /// After routines will be called when <c>true</c>
    /// and suppressed if otherwise.
    /// </returns>
    public abstract bool Handler(Object first, Object second = null);

    public bool Handler()
    {
        return Handler(null, null);
    }

    // score is an example of a game verb. When true, command does not increment game turns.
    public bool IsGameVerb { get; protected set; }

    // if false, parser will consider all objects, not just those in scope
    public bool InScopeOnly { get; protected set; } = true;

    public Func<O, Object> ImplicitObject { get; protected set; } = (O _) => null;
    public Func<Object,string> ImplicitMessage = null;

    /// <summary>
    /// Reverse word order. Normally, you might say "give food to bear", but "give bear food" should also be valid.
    /// Note the lack of a preposition.
    /// </summary>
    /// <remarks>
    /// Should rarely be used. In Inform 6, this is used with Show and Give
    /// </remarks>
    public bool Reverse { get; set; }

    protected static bool Print(string message)
    {
        message = message.Capitalize();
        Output.Print(message);
        return true;
    }

    protected static bool Success(string message)
    {
        message = message.Capitalize();
        Output.Print(message);
        return true;
    }

    protected static bool Fail(string message)
    {
        message = message.Capitalize();
        Output.Print(message);
        return false;
    }

    public bool AcceptsManyObjects => Requires.Any(_multiTypes.Contains);

    public string Verb => Verbs.Count > 0 ? Verbs[0] : throw new Exception("Routine does not have any verbs");

    private string DebuggerDisplay
    {
        get
        {
            return GetType().Name;
        }
    }

    protected void Dead()
    {
        throw new DeathException();
    }

    protected List<Object> ObjectsInScope => CurrentRoom.ObjectsInScope();
}