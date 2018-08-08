namespace Adventure.Net
{
    public class Context
    {
        static Context()
        {
            //Inventory = new Inventory();
        }

        public static IStory Story { get; set; }
        public static Output Output { get; set; }
        public static CommandPrompt CommandPrompt { get; set; }

        public static Object Object { get; set; }
        public static Object IndirectObject { get; set; }

        public static IParser Parser { get; set; }
    }
}
