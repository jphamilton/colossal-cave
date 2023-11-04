using Adventure.Net;
using ColossalCave.Places;

namespace Tests;

public class RedHat : Object
{
    public override void Initialize()
    {
        Name = "red hat";
        Synonyms.Are("red", "hat");
        Clothing = true;
    }
}

public class BlackHat : Object
{
    public override void Initialize()
    {
        Name = "black hat";
        Synonyms.Are("black", "hat");
        Clothing = true;
    }
}

public class WhiteHat : Object
{
    public override void Initialize()
    {
        Name = "white hat";
        Synonyms.Are("white", "hat");
        Clothing = true;
    }
}

public class BlackCloak : Object
{
    public override void Initialize()
    {
        Name = "black cloak";
        Synonyms.Are("black", "cloak", "cape");
        Description = "The cloak is a black darker than black. A color so dark it confuses the eyes.";
        Clothing = true;
    }
}

