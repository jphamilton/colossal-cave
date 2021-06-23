namespace Adventure.Net
{
    public static class MovePlayer
    {
        public static bool To<T>() where T : Room
        {
            var room = Rooms.Get<T>();
            To(room);
            return true;
        }

        public static void To(Room room)
        {
            Room nextRoom = room;

            if (!CurrentRoom.IsLit() && !room.HasLight)
            {
                room = Rooms.Get<Darkness>();
            }

            Context.Story.Location = room;

            if (!room.Visited)
            {
                CurrentRoom.Look(true);

                room.Initial?.Invoke();
            }
            else
            {
                if (!CurrentRoom.IsLit() && room.Visited)
                {
                    nextRoom.DarkToDark();
                }

                CurrentRoom.Look(false);
            }


            room.Visited = true;

            Context.Story.Location = nextRoom;
        }
    }
}
