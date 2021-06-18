using Adventure.Net.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Adventure.Net
{
    public class Rooms
    {
        private static readonly List<Room> rooms = new();

        public static void Load(IStory story)
        {
            rooms.Clear();

            Assembly ax = story.GetType().Assembly;
            rooms.AddRange(ax.SubclassOf<Room>());
            
            ax = Assembly.GetExecutingAssembly();
            rooms.AddRange(ax.SubclassOf<Room>());
        }

        public static IList<Room> All
        {
            get { return rooms; }
        }

        public static Room Get(Type type)
        {
            foreach (var room in rooms)
            {
                Type roomType = room.GetType();
                if (roomType == type)
                    return room;
            }

            return null;
        }

        public static T Get<T>() where T : Room
        {
            return (T)Get(typeof(T));
        }

        public static Room GetByName(string name)
        {
            return rooms.Where(x => x.Name == name || x.Synonyms.Contains(name)).FirstOrDefault();
        }

        public static IList<Room> WithName(string name)
        {
            return rooms.Where(x => x.Name == name || x.Synonyms.Contains(name)).ToList();
        }

        
    }
}
