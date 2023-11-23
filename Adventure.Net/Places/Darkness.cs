namespace Adventure.Net.Places;

public class Darkness : Room
{
    public Darkness()
    {
        Name = "Darkness";
        Light = false;
        Description = "It's pitch black. You can't see a thing.";
    }

    public override void Initialize()
    {
        
    }
}
