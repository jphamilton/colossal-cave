using Adventure.Net.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Adventure.Net
{
    public class Inventory 
    {
        private static readonly List<Item> objects = new List<Item>();

        public static void Add(Item obj)
        {
            objects.Add(obj);
            obj.IsTouched = true;
        }

        public static bool Contains<T>() where T : Item
        {
            Item obj = Net.Objects.Get<T>();
            return Contains(obj);
        }

        public static bool Contains(Item obj)
        {
            foreach(var o in objects)
            {
                Container c = o as Container;
                if (c != null)
                {
                    if (c.Contents.Contains(obj))
                        return true;
                }
            }
            
            return objects.Contains(obj);    
        }

        public static bool Contains(string objName)
        {
            foreach(var obj in objects)
            {
                if (obj.Name == objName || obj.Synonyms.Contains(objName))
                    return true;
            }

            return false;
        }

        public static bool Contains(params Item[] args)
        {
            if (args.Length == 0)
                return false;

            foreach(Item obj in args)
            {
                if (!objects.Contains(obj))
                    return false;
            }

            return true;
        }

        public static void Remove(Item obj)
        {
            objects.Remove(obj);
        }

        public static void Clear()
        {
            objects.Clear();
        }

        public static string Display()
        {
            if (objects.Count == 0)
                return "You are carrying nothing.";

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("You are carrying:");

            var containers = new List<Item>();

            foreach(var obj in objects.OrderBy(x=>x.Description))
            {
                var container = obj as Container;
                if (container != null)
                {
                    containers.Add(container);
                    sb.Append(DisplayContainer(container, 1));
                }
            }

            foreach (var obj in objects.Where(x => !containers.Contains(x)).OrderBy(x => x.Description))
            {
                sb.AppendFormat("\t{0} {1}\n", obj.Article, obj.Name);
            }

            return sb.ToString();
        }

        private static string DisplayContainer(Container container, int level)
        {
            StringBuilder sb = new StringBuilder();

            sb.Indent(level);

            if (container.IsOpenable)
            {
                sb.AppendFormat("{0} {1} ({2})\n", container.Article, container.Name, container.State);
            }
            else
            {
                sb.AppendFormat("{0} {1}\n", container.Article, container.Name);
            }

            if (container.IsOpen || container.IsTransparent)
                foreach(var child in container.Contents)
                {
                    Container c = child as Container;
                    if (c != null)
                        sb.AppendFormat("{0}", DisplayContainer(c, level + 1));
                    else
                    {
                        sb.Indent(level + 1);
                        sb.AppendFormat("{0} {1}\n", child.Article, child.Name);
                    }
                }

            return sb.ToString();
        }

        public static IList<Item> Items
        {
            get { return objects.AsReadOnly(); }
        }

        public static int Count
        {
            get { return objects.Count; }
        }
    }
}
