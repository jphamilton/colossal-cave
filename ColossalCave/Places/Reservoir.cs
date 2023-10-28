using Adventure.Net;
using Adventure.Net.Actions;

namespace ColossalCave.Places;

public class Reservoir : BelowGround
{
    public override void Initialize()
    {
        Name = "At Reservoir";
        Synonyms.Are("reservoir");
        Description =
            "You are at the edge of a large underground reservoir. " +
            "An opaque cloud of white mist fills the room and rises rapidly upward. " +
            "The lake is fed by a stream, which tumbles out of a hole in the wall about 10 feet overhead " +
            "The only passage goes back toward the south. " +
            "and splashes noisily into the water somewhere within the mist.";

        SouthTo<MirrorCanyon>();

        OutTo<MirrorCanyon>();

        Before<Swim>(() =>
        {
            Print("The water is icy cold, and you would soon freeze to death.");
            return true;
        });
    }
}
