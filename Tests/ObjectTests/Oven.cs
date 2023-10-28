using Adventure.Net;

namespace Tests.ObjectTests;

public class Oven : Container
{
    public override void Initialize()
    {
        Name = "oven";
        Open = true;
        Openable = true;
        Transparent = false;
    }
}