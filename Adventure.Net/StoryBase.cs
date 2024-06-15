using Adventure.Net.Things;
using System.Linq;

namespace Adventure.Net;

public abstract class StoryBase : IStory
{
    public string Name { get; set; }
    public bool IsDone { get; set; }
    public int Moves { get; set; } = 0;
    public int CurrentScore { get; set; } = 0;
    public int PossibleScore { get; set; } = 0;
    public bool Verbose { get; set; }

    protected abstract void Start();

    protected StoryBase()
    {
        // derived classes should modify any static library primitives or values in their constructor.
        // For example, adding values to IgnoredWords or ReplacedWords or new Compass directions.

        // instance variables should NOT be set here
    }

    public void Initialize()
    {
        IsDone = false;

        Context.Story.CurrentScore = 0;
        Context.Story.Moves = 0;

        Dictionary.Load();
        Routines.Load();
        Objects.Load(this);
        
        Inventory.Items.Clear();

        foreach (var obj in Objects.All.Where(x => x is Room))
        {
            obj.Initialize();

            if (obj is Door door)
            {
                Dictionary.AddDoor(door);
            }
            else if (obj is Room room)
            {
                Dictionary.AddRoom(room);
            }
        }

        foreach (var obj in Objects.All.Where(x => x is not Room))
        {
            obj.Initialize();
            Dictionary.AddObject(obj);
        }

        Dictionary.Sort();

        Start();

        MovePlayer.To(Player.Location);
    }

}
