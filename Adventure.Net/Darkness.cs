namespace Adventure.Net
{
    public class Darkness : Room
    {
        public override void Initialize()
        {
            Name = "Darkness";
            HasLight = false;
            Description = "It's pitch black. You can't see a thing.";
        }
    }
}
