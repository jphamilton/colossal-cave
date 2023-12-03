using Adventure.Net.Utilities;
using System;

namespace Adventure.Net.Actions;

public class Restart : Verb
{
    public Restart()
    {
        Name = "restart";
        GameVerb = true;
    }

    public bool Expects()
    {
        if (YesOrNo.Ask("Are you sure you want to restart?"))
        {
            Console.Clear();
            Context.Story.Initialize();
        }

        return false;
    }
}
