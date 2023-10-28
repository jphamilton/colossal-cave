namespace ColossalCave.Places;

public class ComplexJunction : BelowGround
{
    public override void Initialize()
    {
        Name = "At Complex Junction";
        Description =
            "You are at a complex junction. " +
            "A low hands and knees passage from the north joins a higher crawl from the east " +
            "to make a walking passage going west. " +
            "There is also a large room above. " +
            "The air is damp here.";

        UpTo<DustyRockRoom>();
        WestTo<Bedquilt>();
        NorthTo<ShellRoom>();
        EastTo<Anteroom>();
    }
}
