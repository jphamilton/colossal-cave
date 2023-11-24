using Adventure.Net;
using Adventure.Net.Actions;
using ColossalCave.Actions;
using ColossalCave.Places;

namespace ColossalCave.Things;

public abstract class Treasure : Object
{
    public int DepositPoints { get; set; }
    public bool Found { get; set; }

    protected Treasure()
    {
        DepositPoints = 10;

        After<Take>(() =>
        {
            int score = 0;

            if (CurrentRoom.Location is InsideBuilding)
            {
                score -= DepositPoints;
            }

            score += 5;

            if (!Found)
            {
                score += 2;
                Found = true;
                Global.State.TreasuresFound++;
            }

            Score.Add(score, true);

            Print("Taken!");

        });

        After<Drop>(() =>
        {
            int score = -5;

            if (CurrentRoom.Location is InsideBuilding)
            {
                score += DepositPoints;
                Print("Safely deposited.");
            }

            Score.Add(score, true);
        });

        After<Insert>(() =>
        {
            Score.Add(-5); // (in case put inside the wicker cage)
        });
    }

}
