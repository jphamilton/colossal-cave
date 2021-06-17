namespace ColossalCave.Places
{
    public class MirrorCanyon : BelowGround
    {
        public override void Initialize()
        {
            Name = "In Mirror Canyon";
            
            Synonyms.Are("mirror", "canyon");
            
            Description =
                "You are in a north/south canyon about 25 feet across. " +
                "The floor is covered by white mist seeping in from the north. " +
                "The walls extend upward for well over 100 feet. " +
                "Suspended from some unseen point far above you, " +
                "an enormous two-sided mirror is hanging parallel to and midway between the canyon walls. " +
                "\r\n\r\n" +
                "A small window can be seen in either wall, some fifty feet up.";

            SouthTo<SecretNSCanyon0>();
            
            NorthTo<Reservoir>();
        }
    }
}

