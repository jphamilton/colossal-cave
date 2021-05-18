using System.Collections.Generic;

namespace Adventure.Net
{
    public interface IStory
    {
        bool IsDone { get; set; }
        Room Location { get; set; }
        string Story { get; set; }
        
        void Initialize();

        void Quit();
    }
}