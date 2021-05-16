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
            Items.Load(this);

            foreach (var obj in Rooms.All)
            {
                obj.Initialize();
            }

            foreach (var obj in Items.All)
            {
                obj.Initialize();
            }

            OnInitialize();
        }
    }
}
