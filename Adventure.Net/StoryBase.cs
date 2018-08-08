using System;

namespace Adventure.Net
{
    public abstract class StoryBase : IStory
    {
        public string Story { get; set; }
        public string Headline { get; set; }
        public Room Location { get; set; }
        public bool IsDone { get; set; }

        protected StoryBase()
        {
            IsDone = false;
        }

        public void Initialize()
        {
            Rooms.Load(this);
            Objects.Load(this);

            foreach (var obj in Rooms.All)
                obj.Initialize();
            
            foreach (var obj in Objects.All)
                obj.Initialize();

            OnInitialize();
            
        }

        protected abstract void OnInitialize();
        
    }
}
