using Adventure.Net.ActionRoutines;
using System.Collections.Generic;
using System;

namespace Adventure.Net;

[Flags]
public enum WordFlags
{
    None = 0,
    Verb = 1,
    Preposition = 2,
    Object = 4,
    Door = 8,
    Room = 16,
    Direction = 32,
}

public static class Dictionary
{
    public static List<string> Verbs { get; private set; } = [];
    public static List<string> Directions { get; private set; } = [];
    public static List<string> Prepositions { get; private set; } = [];
    public static List<string> Objects { get; private set; } = [];
    public static List<string> Rooms { get; private set; } = [];
    public static List<string> Doors { get; private set; } = [];

    public static void Load()
    {
        Verbs = [];
        Directions = [];
        Prepositions = [];
        Objects = [];
        Doors = [];
        Rooms = [];
    }

    public static void Sort()
    {
        Verbs.Sort();
        Directions.Sort();
        Prepositions.Sort();
        Objects.Sort();
        Doors.Sort();
        Rooms.Sort();
    }

    public static bool LookUp(string word, out WordFlags wordFlags)
    {
        wordFlags = WordFlags.None;

        if (Verbs.Contains(word))
        {
            wordFlags |= WordFlags.Verb;
        }

        if (Directions.Contains(word))
        {
            wordFlags |= WordFlags.Direction;
        }

        if (Prepositions.Contains(word))
        {
            wordFlags |= WordFlags.Preposition;
        }

        if (Objects.Contains(word))
        {
            wordFlags |= WordFlags.Object;
        }

        if (Doors.Contains(word))
        {
            wordFlags |= WordFlags.Door;
        }

        if (Rooms.Contains(word))
        {
            wordFlags |= WordFlags.Room;
        }

        return wordFlags != WordFlags.None;
    }

    public static bool IsPreposition(string word)
    {
        return LookUp(word, out WordFlags f) && (f & WordFlags.Preposition) != 0;
    }

    public static bool IsVerb(string word)
    {
        return LookUp(word, out WordFlags f) && (f & WordFlags.Verb) != 0;
    }

    public static void AddRoutine(Routine routine)
    {
        foreach(var verb in routine.Verbs)
        {
            if (!Verbs.Contains(verb))
            {
                Verbs.Add(verb);
            }

            if (routine is Direction direction && !Directions.Contains(verb))
            {
                Directions.Add(verb);
            }

            foreach (var prep in routine.Prepositions)
            {
                if (!Prepositions.Contains(prep))
                {
                    Prepositions.Add(prep);
                }
            }
        }
    }

    public static void AddObject(Object obj)
    {
        foreach(var synonym in obj.Synonyms)
        {
            if (!Objects.Contains(synonym))
            {
                Objects.Add(synonym);

                if (Prepositions.Contains(synonym))
                {
                    throw new Exception($"Object [{obj.Name}] - '{synonym}' is a preposition and cannot be used as an object name.");
                }
            }
        }
    }

    public static void AddDoor(Door obj)
    {
        foreach (var synonym in obj.Synonyms)
        {
            if (!Doors.Contains(synonym))
            {
                Doors.Add(synonym);
            }
        }
    }

    public static void AddRoom(Room obj)
    {
        foreach (var synonym in obj.Synonyms)
        {
            if (!Rooms.Contains(synonym))
            {
                Rooms.Add(synonym);
            }
        }
    }
}
