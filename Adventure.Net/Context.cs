namespace Adventure.Net
{
    public static class Context
    {
        public static IStory Story { get; set; }
        public static Output Output { get; set; }
        public static CommandPrompt CommandPrompt { get; set; }

        public static Item Item { get; set; }
        public static Item IndirectItem { get; set; }

        public static IParser Parser { get; set; }
    }
}
