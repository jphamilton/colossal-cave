using Adventure.Net;
using Adventure.Net.Actions;
using ColossalCave.Places;
using ColossalCave.Actions;

namespace ColossalCave
{
    public abstract class Treasure : Item
    {
        public int DepositPoints { get; protected set; }
        public int Found { get; private set; }

        protected Treasure()
        {
            DepositPoints = 10;

            After<Take>(() => {
                int score = 0;

                if (CurrentRoom.Location is InsideBuilding)
                {
                    score -= DepositPoints;
                }
                else
                {
                    score += 5;
                }

                if (Found == 0)
                {
                    score = score + 2;
                }

                Found++;

                Score.Add(score, true);

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

}
