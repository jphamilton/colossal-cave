namespace Adventure.Net.Places;

public class Darkness : Room
{
    public Darkness()
    {
        Name = "Darkness";
        Light = false;
        Description = "It's pitch dark, and you can't see a thing.";
    }

    public override void Initialize()
    {
        
    }
}
