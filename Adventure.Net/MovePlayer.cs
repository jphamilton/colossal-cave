namespace Adventure.Net
{
    public static class MovePlayer
    {
        public static void To<T>() where T : Room
        {
            var room = Rooms.Get<T>();
            To(room);
        }

        public static void To(Room room)
        {
            Room nextRoom = room;

            if (!CurrentRoom.IsLit() && !room.HasLight)
            {
                room = Rooms.Get<Darkness>();
            }

            Context.Story.Location = room;

            if (!room.Visited && room.Initial != null)
            {
                CurrentRoom.Look(true);
                room.Initial();
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
