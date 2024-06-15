//using Adventure.Net.Things;
//using System;

//namespace Adventure.Net;

//public abstract class Direction : Verb
//{
//    private Func<Room, Room> getRoom;

//    protected void SetDirection(Func<Room, Room> mover, params string[] synonyms)
//    {
//        Name = synonyms[0];
//        getRoom = mover;
//        Synonyms.Are(synonyms);
//    }

//    public bool Expects()
//    {
//        var room = getRoom(Player.Location);

//        if (room == Player.Location)
//        {
//            // do nothing
//        }
//        else if (room == null)
//        {
//            Print(Player.Location.CantGo);
//        }
//        else
//        {
//            MovePlayer.To(room);
//        }

//        return true;
//    }
//}
