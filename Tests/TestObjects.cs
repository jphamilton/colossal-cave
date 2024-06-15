using Adventure.Net;
using Adventure.Net.ActionRoutines;

namespace Tests;

public class HeavyBoots : Object
{
    public override void Initialize()
    {
        Name = "heavy boots";
        Synonyms.Are("heavy", "boots");
        Clothing = true;

        Before<Take>(() => Print("The boots are too heavy."));
    }
}

public class Donkey : Object
{
    public override void Initialize()
    {
        Name = "donkey";
        Synonyms.Are("donkey");
        Animate = true;
        Description = "A donkey is here, looking for a friend.";
    }
}

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

public class Table : Supporter
{
    public override void Initialize()
    {
        Name = "rustic wooden table";
        Synonyms.Are("rustic", "wooden", "table");
        Description = "Just an old farmhouse table that's seen better years.";
    }
}

public class OrangeBox : Container
{
    public override void Initialize()
    {
        Name = "strange ornate box";
        Synonyms.Are("strange", "ornate", "box");
        Description = "The box is just strange, ok?";
        Transparent = true;
        Openable = true;
        Open = true;
    }
}

public class OpaqueBox : Container
{
    public override void Initialize()
    {
        Name = "opaque box";
        Synonyms.Are("opaque", "box");
        Description = "The box looks like a solid cube made of light.";
        Transparent = false;
        Openable = true;
        Open = true;
    }
}

public class MagentaBox : Container
{
    public override void Initialize()
    {
        Name = "magenta box";
        Synonyms.Are("magenta", "box");
        Description = "It's just a box";
        Transparent = true;
        Openable = true;
        Open = true;
    }
}