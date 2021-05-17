using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Adventure.Net
{
    public static class Compass
    {
        private static readonly Collection<string> directions = null;

        static Compass()
        {
            var list = new List<string>
            {
                "n",
                "north",
                "s",
                "south",
                "e",
                "east",
                "w",
                "west",
                "nw",
                "northwest",
                "sw",
                "southwest",
                "ne",
                "northeast",
                "se",
                "southeast",
                "u",
                "up",
                "d",
                "down",
                "in",
                "out",
                "enter"
            };

            directions = new Collection<string>(list);
        }

        public static IList<string> Directions
        {
            get { return directions; }
        }
    }
}
