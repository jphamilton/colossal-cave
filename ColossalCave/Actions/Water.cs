using Adventure.Net;

namespace ColossalCave.Actions
{
    public class Water : Verb
    {
        public Water()
        {
            Name = "water";
        }

        public bool Expects(Object obj)
        {
            return true;
        }
    }
}

/*
 [ WaterSub;
    if (bottle in player) <<Empty bottle>>;
    "Water? What water?";
];

[ OilSub;
    if (bottle in player) <<Empty bottle>>;
    "Oil? What oil?";
];

Verb 'water'
    * noun                  -> Water;

Verb 'oil' 'grease' 'lubricate'
    * noun                  -> Oil;

Verb 'pour' 'douse'
    * 'water' 'on' noun     -> Water
    * 'oil' 'on' noun       -> Oil
    * noun                  -> Empty;

 */