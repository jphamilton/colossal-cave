using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Adventure.Net
{
    public class Library 
    {

        public Library()
        {
            CantSeeObject = "You can't see any such thing.";
            DoNotUnderstand = "I beg your pardon?";
            DidntUnderstandSentence = "I didn't understand that sentence.";
            VerbNotRecognized = "That's not a verb I recognize.";
        }

        public void Banner()
        {
            Bold(Story.Story);
            Print(Story.Headline);
            PrintLine();
        }

        public void Bold(string message)
        {
            Context.Output.Bold(message);
        }

        public void Print(string msg)
        {
            Context.Output.Print(msg);
        }

        public void Print(string format, params object[] arg)
        {
            Context.Output.Print(format, arg);
        }

        public void PrintLine()
        {
            Context.Output.PrintLine();
        }

        private Room Location
        {
            get { return Context.Story.Location; }
        }

        private IStory Story
        {
            get { return Context.Story; }
        }

        public string DoNotUnderstand { get; set;}
        public string DidntUnderstandSentence { get; set;}
        public string CantSeeObject { get; set; }
        public string VerbNotRecognized { get; set; }

        public void Look(bool showFull)
        {
            PrintLine();

            Room room = IsLit() ? Location : Rooms.Get<Darkness>();

            Bold(room.Name);

            if (showFull || !Location.Visited)
                Print(room.Description);

            DisplayRoomObjects();

        }

        public bool IsLit()
        {
            if (Location.HasLight)
                return true;

            foreach(var obj in Inventory.Items)
            {
                if (obj.HasLight)
                    return true;
                var container = obj as Container;
                if (container == null || (!container.IsOpen && !container.IsTransparent)) 
                    continue;
                foreach(var containedObj in container.Contents)
                {
                    if (containedObj.HasLight)
                        return true;
                }
            }

            return false;
        }

        private void DisplayRoomObjects()
        {
            var ordinary = new List<Object>();
            int total = 0;

            foreach(var obj in Location.Objects)
            {
                if (obj.IsScenery && obj.Describe == null)
                    continue;

                if (obj.IsStatic && obj.Describe == null)
                    continue;

                total++;

                if (!obj.IsTouched && !String.IsNullOrEmpty(obj.InitialDescription))
                {
                    PrintLine();
                    Print(obj.InitialDescription);
                }
                else if (obj.Describe != null && (obj as Container) == null)
                {
                    PrintLine();
                    Print(obj.Describe());
                }
                else
                    ordinary.Add(obj);
            }

            var group = new StringBuilder();

            if (total > ordinary.Count)
                group.Append("You can also see ");
            else
                group.Append("You can see ");

            for(int i = 0; i < ordinary.Count; i++)
            {
                Object obj = ordinary[i];

                if (i == ordinary.Count - 1 && i > 0)
                    group.Append(" and ");
                else if (i > 0)
                    group.Append(", ");

                var container = obj as Container;
                if (container != null)
                {
                    if (container.Contents.Count > 0)
                    {
                        Object child = container.Contents[0];
                        group.AppendFormat("{0} {1} (which contains {2} {3})", obj.Article, obj.Name, child.Article, child.Name);
                    }
                    else
                        group.AppendFormat("{0} {1} (which is empty)", obj.Article, obj.Name);
                }
                else
                {
                    group.AppendFormat("{0} {1}", obj.Article, obj.Name);
                }
                    
            }

            group.Append(" here.");

            if (ordinary.Count > 0)
            {
                PrintLine();
                Print(group.ToString());
            }
            
    
        }

        public Room CurrentLocation
        {
            get { return Context.Story.Location; }
        }

        public void Quit()
        {
            if (YesOrNo("Are you sure you want to quit?"))
            {
                Story.IsDone = true;
            }
        }

        public bool YesOrNo(string question)
        {
            Print(question);

            while (true)
            {
                string[] affirmative = new[]{"y", "yes", "yep", "yeah"};
                string[] negative = new[]{"n", "no", "nope", "nah", "naw"};
                string response = Context.CommandPrompt.GetInput();
                if (!response.In(affirmative) && !response.In(negative))
                    Print("Please answer yes or no.");
                else
                    return (response.In(affirmative));
            }
        }

        public void MovePlayerTo<T>() where T:Room
        {
            var room = Rooms.Get<T>();
            MovePlayerTo(room);
        }

        public void MovePlayerTo(Room room)
        {
            Room real = room;

            if (!IsLit())
                room = Rooms.Get<Darkness>();
            
            Story.Location = room;

            if (!room.Visited && room.Initial != null)
                room.Initial();
            else
            {
                if (!IsLit() && room.Visited)
                    real.DarkToDark();
                Look(false);
            }
                
            
            room.Visited = true;

            Story.Location = real;
        }

        public void RunDaemons()
        {
            IList<Object> objectsWithDaemons = Objects.WithRunningDaemons();
            foreach (var obj in objectsWithDaemons)
                obj.Daemon();
        }

        public Object GetObjectByName(string name)
        {
            var objects = from x in ObjectsInScope() where x.Name == name || x.Synonyms.Contains(name) select x;
            if (objects.Count() > 1)
            {
                //TODO: more than one object in scope with the same name
                //throw new Exception("There is more than one object in scope with the same name - Need to implement!!!!");
                foreach(var obj in objects)
                {
                    if (Inventory.Contains(obj))
                        return obj;
                }
            }

            return objects.FirstOrDefault();
        }

        public List<Object> ObjectsInScope()
        {
            var result = new List<Object>();
            
            result.AddRange(Location.Objects.Where(x => !x.IsScenery && !x.IsStatic));
            result.AddRange(Location.Objects.Where(x => x.IsScenery || x.IsStatic));
            result.AddRange(Inventory.Items);
            result.Add(Location);

            var contained = new List<Object>();
            
            foreach(var obj in result)
            {
                var container = obj as Container;
                if (container != null)
                {
                    contained.AddRange(container.Contents);
                }
            }

            result.AddRange(contained);

            return result;
        }
    }
}
