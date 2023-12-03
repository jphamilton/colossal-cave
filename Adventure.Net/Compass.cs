using System.Collections.Generic;

namespace Adventure.Net;

public static class Compass
{
    private static readonly List<string> directions =
        [
            "north",
            "south",
            "east",
            "west",
            "northwest",
            "southwest",
            "northeast",
            "southeast",
            "up",
            "down",
            "in",
            "out",
            "enter"
        ];

    public static IList<string> Directions
    {
        get { return directions; }
    }
}
