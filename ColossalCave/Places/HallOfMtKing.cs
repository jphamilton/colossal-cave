using Adventure.Net;
using Adventure.Net.Actions;
using Adventure.Net.Utilities;
using ColossalCave.Things;


namespace ColossalCave.Places;

public class HallOfMtKing : BelowGround
{
    public override void Initialize()
    {
        Name = "Hall of the Mountain King";
        Description = "You are in the hall of the mountain king, with passages off in all directions.";
        CantGo = "Well, perhaps not quite all directions.";

        UpTo<HallOfMists>();
        EastTo<HallOfMists>();
        NorthTo<LowNSPassage>();
        SouthTo<SouthSideChamber>();
        WestTo<WestSideChamber>();
        SouthWestTo<SecretEWCanyon>();

        Before<Go>((Direction direction) =>
        {
            var snake = Objects.Get<Snake>();

            var totallyBlocked = direction is North || direction is South || direction is West;
            var randomlyBlocked = direction is Southwest && Random.Number(1, 100) <= 35;

            if (snake.InRoom && (totallyBlocked || randomlyBlocked))
            {
                Print("You can't get by the snake.");
                return true;
            }

            return false;
        });
    }
}
