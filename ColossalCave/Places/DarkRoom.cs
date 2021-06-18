using Adventure.Net;

namespace ColossalCave.Places
{
    public class DarkRoom : BelowGround
    {
        public override void Initialize()
        {
            Name = "The Dark Room";
            Synonyms.Are("dark", "room");
            Description = "You're in the dark-room. A corridor leading south is the only exit.";
            NoDwarf = true;

            SouthTo<PloverRoom>();
        }
    }

    public class StoneTablet : Item
    {
        public override void Initialize()
        {
            Name = "stone tablet";
            Synonyms.Are("tablet", "massive", "stone");
            Description =
                "A massive stone tablet imbedded in the wall reads:\r\n"+
                "\"Congratulations on bringing light into the dark-room!\"";
            IsStatic = true;

            FoundIn<DarkRoom>();
        }
    }
}
