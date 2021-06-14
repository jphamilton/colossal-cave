using Adventure.Net;

namespace ColossalCave
{
    public static class Global
    {
        public static bool CavesClosed { get; set; }
        public static Room CanyonFrom { get; set; }
        public static int TreasuresFound { get; set; }
        public static int Deaths { get; set; } = 0;
        public static bool DarkWarning { get; set; }
        public static int FeeFieCount { get; set; }
    }
}

