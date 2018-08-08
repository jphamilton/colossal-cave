using System.Collections.Generic;

namespace Adventure.Net
{
    public static class Compass
    {
        private static readonly List<string> directions = new List<string>();

        static Compass()
        {
            directions.AddRange(new List<string>
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
                                    });
        }

        public static IList<string> Directions
        {
            get { return directions; }
        }
    }
}
