using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Adventure.Net
{
    public static class CurrentRoom
    {
        public static Room Location
        {
            get { return Context.Story.Location; }
        }

        //public static IList<Item> Objects
        //{
        //    get { return Location.Contents; }
        //}

        public static void Look(bool showFull)
        {
            // look is special in that it uses extra formatting like bold,
            // so this is sent directly to the console

            Output.PrintLine();

            Room room = IsLit() ? Location : Rooms.Get<Darkness>();

            Output.Bold(room.Name);

            if (showFull || !Location.Visited)
            {
                Output.Print(room.Description);
            }

            DisplayRoomObjects();

        }

        public static bool IsLit()
        {
            if (Location.HasLight)
                return true;

            var objects = ObjectMap.GetObjects(Location);

            if (objects.Any(obj => obj.HasLight))
            {
                return true;
            }

            foreach (var obj in Inventory.Items)
            {
                if (obj.HasLight)
                    return true;
                
                var container = obj as Container;
                
                if (container == null || (!container.IsOpen && !container.IsTransparent))
                    continue;
                
                foreach (var containedObj in container.Contents)
                {
                    if (containedObj.HasLight)
                        return true;
                }
            }

            return false;
        }

        private static void DisplayRoomObjects()
        {
            var ordinary = new List<Item>();
            int total = 0;

            var objects = ObjectMap.GetObjects(Location);

            foreach (var obj in objects)
            {
                if (obj.IsScenery && obj.Describe == null)
                    continue;

                if (obj.IsStatic && (obj.Describe == null && obj.InitialDescription == null))
                    continue;

                total++;

                if (!obj.IsTouched && !string.IsNullOrEmpty(obj.InitialDescription))
                {
                    Output.PrintLine();
                    Output.Print(obj.InitialDescription);
                }
                else if (obj.Describe != null && (obj as Container) == null)
                {
                    Output.PrintLine();
                    Output.Print(obj.Describe());
                }
                else
                {
                    ordinary.Add(obj);
                }
            }

            var group = new StringBuilder();

            if (total > ordinary.Count)
                group.Append("You can also see ");
            else
                group.Append("You can see ");

            for (int i = 0; i < ordinary.Count; i++)
            {
                Item obj = ordinary[i];

                if (i == ordinary.Count - 1 && i > 0)
                    group.Append(" and ");
                else if (i > 0)
                    group.Append(", ");

                var container = obj as Container;
                if (container != null)
                {
                    if (container.Contents.Count > 0)
                    {
                        Item child = container.Contents[0];
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
                Output.PrintLine();
                Output.Print(group.ToString());
            }
        }

        private static void AddContained(List<Item> objects)
        {
            var contained = new List<Item>();

            foreach (var obj in objects)
            {
                var container = obj as Container;
                
                if (container != null)
                {
                    contained.AddRange(container.Contents);
                }
            }

            objects.AddRange(contained);
        }

        public static IList<Item> ObjectsInRoom()
        {
            var result = new List<Item>();
            var objects = ObjectMap.GetObjects(Location);

            result.AddRange(objects.Where(x => !x.IsScenery && !x.IsStatic));
            result.AddRange(objects.Where(x => x.IsScenery || x.IsStatic));
            AddContained(result);
            return result;
        }

        public static List<Item> ObjectsInScope()
        {
            var result = new List<Item>();

            var objects = ObjectMap.GetObjects(Location);

            result.AddRange(objects.Where(x => !x.IsScenery && !x.IsStatic));
            result.AddRange(objects.Where(x => x.IsScenery || x.IsStatic));
           
            result.AddRange(Inventory.Items);
            
            // note: location is added to scope to support things like Door
            result.Add(Location);

            AddContained(result);

            return result;
        }

        public static bool Is<T>() where T : Room
        {
            return Location.GetType() == typeof(T);
        }

        public static bool Has<T>() where T : Item
        {
            return Location.Contains<T>();
        }
    }
}
