using Adventure.Net.Extensions;

namespace Adventure.Net
{
    public abstract class StoryBase : IStory
    {
        public string Story { get; set; }
        public Room Location { get; set; }
        public bool IsDone { get; set; }

        protected abstract void OnInitialize();

        protected StoryBase()
        {
            IsDone = false;
        }

        public void Initialize()
        {
            Rooms.Load(this);
            Objects.Load(this);

            foreach (var obj in Rooms.All)
            {
                obj.Initialize();
            }

            foreach (var obj in Objects.All)
            {
                obj.Initialize();
            }

            OnInitialize();
        }

        public virtual void Quit()
        {
            if (YesOrNo("Are you sure you want to quit?"))
            {
                IsDone = true;
            }
        }

        // TODO: This needs to go somewhere else
        private static bool YesOrNo(string question)
        {
            Output.Print(question);

            while (true)
            {
                string[] affirmative = new[] { "y", "yes", "yep", "yeah" };
                string[] negative = new[] { "n", "no", "nope", "nah", "naw", "nada" };
                string response = CommandPrompt.GetInput();
                if (!response.In(affirmative) && !response.In(negative))
                    Output.Print("Please answer yes or no.");
                else
                    return (response.In(affirmative));
            }
        }
    }
}
