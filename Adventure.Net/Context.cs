namespace Adventure.Net
{
    public static class Context
    {
        public static IStory Story { get; set; }

        public static CommandContext Current { get; set; }
    }
}
