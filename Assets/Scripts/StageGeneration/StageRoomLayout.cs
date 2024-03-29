using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StageRoomLayout : MonoBehaviour
{
    public int roomAmount = 9;
    [HideInInspector]
    public Room[] latestRooms;
    [HideInInspector]
    public int[] doorAmounts;

    public Room[] GenerateRoomLayout()
    {
        Debug.Log("Generating new Layout");

        // Instatiate all rooms
        Room[] rooms = new Room[roomAmount];
        for (int i = 0; i <= 2; i++)
        {
            for (int j = 0; j <= 2; j++)
            {
                Vector2Int pos = new Vector2Int(j, i);
                rooms[GetID(pos)] = new Room(pos);
            }
        }

        // Add connections (walls if you so will) between rooms (NO DOORS YET!)
        foreach (Room room in rooms)
        {
            if (IsInClamp(room.position + Vector2Int.up, 0, 2))
                room.flanks.Add(rooms[GetID(room.position + Vector2Int.up)]);
            if (IsInClamp(room.position + Vector2Int.right, 0, 2))
                room.flanks.Add(rooms[GetID(room.position + Vector2Int.right)]);
            if (IsInClamp(room.position + Vector2Int.down, 0, 2))
                room.flanks.Add(rooms[GetID(room.position + Vector2Int.down)]);
            if (IsInClamp(room.position + Vector2Int.left, 0, 2))
                room.flanks.Add(rooms[GetID(room.position + Vector2Int.left)]);
        }

        // Randomize start and end room
        int startRoomID = RandomInt(0, roomAmount);
        int endRoomID = startRoomID;
        while(startRoomID == endRoomID)
            endRoomID = RandomInt(0, roomAmount);
        Room startRoom = rooms[startRoomID];
        Room endRoom = rooms[endRoomID];

        // Setup start and end room
        startRoom.isStartRoom = true;
        endRoom.isEndRoom = true;
        startRoom.roomLevel = 0;
        endRoom.roomLevel = 0;

        // Randomize room Levels
        // ---------------------
        // Define remaining bronze rooms (start and end are already set)
        int bronzeRoomAmount = 2;
        while (bronzeRoomAmount < 5)
        {
            RandomRoomOfType(rooms, -1).roomLevel = 0;
            bronzeRoomAmount++;
        }

        // Place last bronze room (some positions invalid, because end not reachable through bronze rooms only)
        Room additionalBronzeRoom = RandomRoomOfType(rooms, -1);
        additionalBronzeRoom.roomLevel = 0;
        while (!IsReachableByBronze(startRoom))
        {
            additionalBronzeRoom.roomLevel = -1;
            additionalBronzeRoom = RandomRoomOfType(rooms, -1);
            additionalBronzeRoom.roomLevel = 0;
        }

        // Define silver rooms
        int silverRoomAmount = 0;
        while (silverRoomAmount < 2)
        {
            RandomRoomOfType(rooms, -1).roomLevel = 1;
            silverRoomAmount++;
        }

        // Define gold room
        RandomRoomOfType(rooms, -1).roomLevel = 2;

        // Add doors
        List<Room> silverRooms = new List<Room>(); // For later key assignment

        foreach (Room room in rooms)
        {
            // If bronze room
            if (room.roomLevel == 0)
            {
                room.keys[0] = 1; // Give it possibility to drop bronze key

                // Add bronze door between bronze rooms
                if (IsInClamp(room.position + Vector2Int.up, 0, 2) && (rooms[GetID(room.position + Vector2Int.up)].roomLevel == 0))
                {
                    room.doors[0, 0] = rooms[GetID(room.position + Vector2Int.up)];
                    room.doors[0, 1] = 0;
                }
                if (IsInClamp(room.position + Vector2Int.right, 0, 2) && (rooms[GetID(room.position + Vector2Int.right)].roomLevel == 0))
                {
                    room.doors[1, 0] = rooms[GetID(room.position + Vector2Int.right)];
                    room.doors[1, 1] = 0;
                }
                if (IsInClamp(room.position + Vector2Int.down, 0, 2) && (rooms[GetID(room.position + Vector2Int.down)].roomLevel == 0))
                {
                    room.doors[2, 0] = rooms[GetID(room.position + Vector2Int.down)];
                    room.doors[2, 1] = 0;
                }
                if (IsInClamp(room.position + Vector2Int.left, 0, 2) && (rooms[GetID(room.position + Vector2Int.left)].roomLevel == 0))
                {
                    room.doors[3, 0] = rooms[GetID(room.position + Vector2Int.left)];
                    room.doors[3, 1] = 0;
                }
            }
            // If silver room
            else if (room.roomLevel == 1)
            {
                silverRooms.Add(room); // For later key assignment

                // Add max two silver doors
                List<int> doorFlankOrder = Range(room.flanks.Count);
                ShuffleList(doorFlankOrder);
                int silverDoorAmount = 0;
                foreach (int flank in doorFlankOrder)
                {
                    Room flankRoom = room.flanks[flank];
                    // Silver door connection to bronze rooms an skip add trials after 2 adds
                    if (silverDoorAmount >= 2)
                        break;
                    if (flankRoom.roomLevel > 1)
                        continue;
                    silverDoorAmount++;
                    int direction = GetIDByDirection(flankRoom.position - room.position);
                    // Set silver door from silver room
                    room.doors[direction, 0] = flankRoom;
                    room.doors[direction, 1] = 1;
                    // Set silver door from bronze room
                    flankRoom.doors[(direction + 2) % 4, 0] = room;
                    flankRoom.doors[(direction + 2) % 4, 1] = 1;
                }
            }
            // If gold room
            else if (room.roomLevel == 2)
            {
                List<int> doorFlankOrder = Range(room.flanks.Count);
                ShuffleList(doorFlankOrder);
                foreach (int flank in doorFlankOrder)
                {
                    Room flankRoom = room.flanks[flank];
                    int direction = GetIDByDirection(flankRoom.position - room.position);
                    // Set gold door in gold room
                    room.doors[direction, 0] = flankRoom;
                    room.doors[direction, 1] = 2;
                    // Set gold door from bronze room
                    flankRoom.doors[(direction + 2) % 4, 0] = room;
                    flankRoom.doors[(direction + 2) % 4, 1] = 2;
                    break;
                }
            }
        }

        // Determine door amounts
        doorAmounts = GetDoorAmount(rooms);

        // Give silver keys to two random bronze rooms
        List<Room> reachableBronzeRooms = ReachableRoomsByBronze(startRoom);
        ShuffleList(reachableBronzeRooms);
        for (int i = 0; i < 2; i++)
        {
            reachableBronzeRooms[i].keys[1] = 1;
        }

        // Distribute remaining keys for silver doors in silver rooms
        ShuffleList(silverRooms);
        for (int i = 0; i < doorAmounts[1] - 2; i++)
        {
            silverRooms[i].keys[1] = 1;
        }

        // Put one gold key into one of the silver rooms
        silverRooms[(int)Random.Range(0, 2)].keys[2] = 1;

        latestRooms = (Room[]) rooms.Clone();
        return rooms;
    }

    private int GetID(Vector2Int pos)
    {
        return pos.x + pos.y * 3;
    }

    private bool IsInClamp(Vector2Int pos, int low, int high)
    {
        return (Mathf.Clamp(pos.x, low, high) == pos.x) && (Mathf.Clamp(pos.y, low, high) == pos.y);
    }

    private int RandomInt(int min, int max)
    {
        // Includes only min, not max
        return Mathf.FloorToInt(Random.Range(min, max));
    }

    private Room randomRoom(Room[] rooms)
    {
        return rooms[RandomInt(0, roomAmount)];
    }

    private Room RandomRoomOfType(Room[] rooms, int type)
    {
        Room newRoom = randomRoom(rooms);
        while (newRoom.roomLevel != type)
            newRoom = randomRoom(rooms);
        return newRoom;
    }

    private bool IsReachableByBronze(Room startRoom)
    {
        List<Room> roomsToCheck = new List<Room> { startRoom };
        bool[] roomChecked = new bool[roomAmount];
        while (roomsToCheck.Count > 0) // Loop through all rooms connected to startRoom
        {
            Room room = roomsToCheck[0];
            if (room.isEndRoom) // endRoom is reached -> isReachableByBronze: true
                return true;

            roomsToCheck.RemoveAt(0);
            roomChecked[room.GetID()] = true;

            foreach (Room flankRoom in room.flanks) // Add bronze rooms to the sides 
            {
                if (roomChecked[flankRoom.GetID()]) // Already checked
                    continue;
                if (flankRoom.roomLevel != 0)
                    continue;
                roomsToCheck.Add(flankRoom);
            }
        }
        return false; // No connection
    }

    private List<Room> ReachableRoomsByBronze(Room startRoom)
    {
        List<Room> reachableBronzeRooms = new List<Room> { startRoom };
        HashSet<int> queuedRoomIDs = new HashSet<int> { startRoom.GetID() };
        int currentRoom = 0;
        while (currentRoom < reachableBronzeRooms.Count) // Loop through all rooms connected to startRoom
        {
            //foreach (Room r in reachableBronzeRooms)
            //{
            //    if (r.getID() == currentRoom)
            //        Debug.LogWarning("Current: " + currentRoom);
            //    Debug.Log("rID: " + r.getID() + " rPos: " + r.position + " rLevel: " + r.roomLevel + " rFlanks " + r.flanks.Count);
            //}
            
            Room room = reachableBronzeRooms[currentRoom];

            foreach (Room flankRoom in room.flanks) // Add bronze rooms to the sides 
            {
                if (queuedRoomIDs.Contains(flankRoom.GetID())) // Already in queue
                    continue;
                if (flankRoom.roomLevel != 0) // Only Bronze
                    continue;
                reachableBronzeRooms.Add(flankRoom);
                queuedRoomIDs.Add(flankRoom.GetID());
            }
            currentRoom++;
        }
        return reachableBronzeRooms;
    }

    private void ShuffleList<T>(List<T> list)
    {
        System.Random random = new System.Random();

        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = random.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }

    private static List<int> Range(int n)
    {
        List<int> result = new List<int>();

        for (int i = 0; i < n; i++)
        {
            result.Add(i);
        }

        return result;
    }

    private int GetIDByDirection(Vector2Int direction)
    {
        if (direction == Vector2Int.up)
            return 0;
        if (direction == Vector2Int.right)
            return 1;
        if (direction == Vector2Int.down)
            return 2;
        if (direction == Vector2Int.left)
            return 3;
        return -1;
    }

    public int[] GetDoorAmount(Room[] rooms)
    {
        /*
         Gets the amount of door pairs types given a array of rooms 
         (returns array where doors[i] for i = 0, 1, 2 -> bronze, silver, gold)
        */
        doorAmounts = new int[3];
        foreach (Room room in rooms)
        {
            for (int i = 0; i < 4; i++) // Check all door direction for door types
            {
                if ((int)room.doors[i, 1] != -1)
                    doorAmounts[(int)room.doors[i, 1]]++;
            }
        }
        for (int i = 0; i < doorAmounts.Length; i++)
        {
            doorAmounts[i] /= 2; // Each door has two parts
        }
        return doorAmounts;
    }

}