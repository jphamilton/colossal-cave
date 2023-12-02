using Adventure.Net.Utilities;
using System.Runtime.CompilerServices;

namespace Adventure.Net.Actions;

public class Quit : Verb
{
    public Quit()
    {
        Name = "quit";
        Synonyms.Are("q");
        GameVerb = true;
    }

    public bool Expects()
    {
        if (YesOrNo.Ask("Are you sure you want to quit?"))
        {
            Context.Story.IsDone = true;
        }

        return false;
    }

}
