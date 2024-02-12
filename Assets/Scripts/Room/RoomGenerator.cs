using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomGenerator : MonoBehaviour
{
    public Room r;
    public int roomID;
    public int roomLevel;
    public GameObject floor;
    public GameObject[] walls = new GameObject[4];
    public GameObject[] doors = new GameObject[4];
    
    // Start is called before the first frame update
    public void Setup(Room roomBlueprint)
    {
        r = roomBlueprint;
        roomID = r.getID();
        roomLevel = r.roomLevel;

        // Get references
        floor = transform.Find("Floor").gameObject;
        Transform wallParent = transform.Find("Walls");
        Transform doorParent = transform.Find("Doors");
        string[] directions = new string[4] { "Up", "Right", "Down", "Left" };
        for (int i = 0; i < 4; i++)
        {
            walls[i] = wallParent.Find($"Wall{directions[i]}").gameObject;
            doors[i] = doorParent.Find($"Door{directions[i]}").gameObject;
        }

        // Colorcode the Room Elements according to level
        //floor.GetComponent<SpriteRenderer>().color = getLevelColor(r.roomLevel);
        for (int i = 0; i < 4; i++)
        {
            doors[i].GetComponent<SpriteRenderer>().color = brightenColor(getLevelColor((int) r.doors[i, 1]));
        }

        // Assign keys
        RoomKeyManager keyManager = GetComponent<RoomKeyManager>();
        for (int i = 0;i < 3; i++)
        {
            keyManager.keys[i] = r.keys[i];
        }
    }

    public void LinkDoors(GameObject[] rooms)
    {
        // Link doors
        for (int i = 0; i < 4; i++)
        {
            if ((int)r.doors[i, 1] == -1) // Skip direction if no door there
                continue;
            int linkedRoomID = ((Room)r.doors[i, 0]).getID();
            RoomGenerator linkedRoom = rooms[linkedRoomID].GetComponent<RoomGenerator>();
            doors[i].GetComponent<DoorOperation>().counterPart = linkedRoom.doors[(i + 2) % 4];
        }
    }

    private Color brightenColor(Color color)
    {
        float brighten(float x)
        {
            return Mathf.Clamp01(Mathf.Sqrt(x + 0.1f));
        }
        return new Color(brighten(color.r), brighten(color.g), brighten(color.b), color.a);
    }

    private Color getLevelColor(int level)
    {
        switch (level)
        {
            case 0:
                return Gizmos.color = new Color(145f/255, 89f/255, 42f/255);
            case 1:
                return Color.gray;
            case 2:
                return Color.yellow;
        }
        return new Color(0f, 0f, 0f, 0f);
    }
}
