using System.Collections.Generic;

namespace Adventure.Net.Actions;

// useful for testing
public class GoNear : ResolveObjects
{
    public GoNear()
    {
        Name = "gonear";
    }

    public override bool Handle(List<Object> objects)
    {
        var obj = objects[0];

        if (obj is Room room)
        {
            Context.Story.Location = room.Location;
            CurrentRoom.Look(true);
        }
        else
        {
            var parent = obj.Parent;
            while (parent != null)
            {
                if (parent is Room room2)
                {
                    Context.Story.Location = room2;
                    CurrentRoom.Look(true);
                    break;
                }

                parent = parent.Parent;
            }
        }

        return true;
    }
}
