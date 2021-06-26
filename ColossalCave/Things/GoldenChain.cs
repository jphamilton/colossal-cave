using Adventure.Net;
using Adventure.Net.Actions;
using ColossalCave.Places;

namespace ColossalCave.Things
{
    public class GoldenChain : Treasure
    {
        public override void Initialize()
        {
            Name = "";
            Synonyms.Are("");
            Description = "The chain has thick links of solid gold!";
            DepositPoints = 14;

            LocksWithKey<SetOfKeys>(true);

            FoundIn<BarrenRoom>();

            Describe = () =>
            {
                if (Locked)
                {
                    return "The bear is held back by a solid gold chain.";
                }

                return "A solid golden chain lies in coils on the ground!";
            };

            Before<Take>(() =>
            {
                if (Locked)
                {
                    var bear = Get<Bear>();
                    
                    if (bear.IsFriendly)
                    {
                        return Print("It's locked to the friendly bear.");
                    }

                    return Print("It's locked to the ferocious bear!");
                }

                return false;
            });

            Before<Unlock>(() =>
            {
                var bear = Get<Bear>();

                if (!bear.IsFriendly)
                {
                    return Print("There is no way to get past the bear to unlock the chain, which is probably just as well.");
                }

                return false;
            });

            Before<Lock>(() =>
            {
                if (!Locked)
                {
                    return Print("The mechanism won't lock again.");
                }

                return false;
            });

            After<Unlock>(() => "You unlock the chain, and set the tame bear free.");
        }
    }
}
