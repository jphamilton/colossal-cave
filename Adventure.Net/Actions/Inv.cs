﻿namespace Adventure.Net.Actions;

public class Inv : Verb
{
    public Inv()
    {
        Name = "inventory";
        Synonyms.Are("i", "inv", "inventory");
    }

    public bool Expects()
    {
        Print(Inventory.Display());
        return true;
    }
}
