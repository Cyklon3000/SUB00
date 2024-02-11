using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room
{
    public Vector2Int position { get; set; }
    public List<Room> flanks { get; set; } = new List<Room>(); // Saved adress to neigboring room
    public object[,] doors { get; set; } = new object[4, 2]; // Saved adress to room with door and door type
    public int[] keys { get; set; } = new int[3]; // keys[i] for i = 0 - 2 -> Bronze - Gold key amount
    public int roomLevel { get; set; } = -1; // -1 not yet set; 0 - 2 Bronze - Gold
    public bool isStartRoom { get; set; } = false;
    public bool isEndRoom { get; set; } = false;

    public Room(Vector2Int pos)
    {
        position = pos;
        for (int i = 0; i < 4; i++)
        {
            doors[i, 0] = null;
            doors[i, 1] = -1;
        }
        for (int i = 0; i < 3; i++)
        {
            keys[i] = 0;
        }
    }

    public int getID()
    {
        return position.x + position.y * 3;
    }
}
