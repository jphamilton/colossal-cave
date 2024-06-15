using Adventure.Net;
using Adventure.Net.ActionRoutines;

namespace Tests.ObjectTests;

public class Oven : Container
{
    public override void Initialize()
    {
        Name = "dirty old oven";
        Synonyms.Are("oven");
        Open = true;
        Openable = true;
        Transparent = false;

        
    }
    
}