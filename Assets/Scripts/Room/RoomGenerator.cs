using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SocialPlatforms.GameCenter;
using UnityEngine.Tilemaps;
using static UnityEngine.Rendering.DebugUI;

public class RoomGenerator : MonoBehaviour
{
    public Room r;
    public int roomID;
    public int roomLevel;
    public GameObject floor;
    public GameObject[] walls = new GameObject[4];
    public GameObject[] doors = new GameObject[4];

    // Start is called before the first frame update
    public void Setup(Room roomBlueprint, int level)
    {
        r = roomBlueprint;
        roomID = r.GetID();
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
            doors[i].GetComponent<SpriteRenderer>().color = brightenColor(GetLevelColor((int)r.doors[i, 1]));
            doors[i].GetComponent<DoorOperation>().doorLevel = (int)r.doors[i, 1];
        }

        TileBase[] originalTiles = GameObject.Find("PrefabCollector").GetComponent<PrefabManager>().windowTiles;
        TileBase replacementTile = GameObject.Find("PrefabCollector").GetComponent<PrefabManager>().wallTile;

        // Replace window tiles of walls with default tiles on inside walls
        for (int i = 0; i < 4; i++)
        {
            if (!IsRoomBehindWall(i)) continue;

            // Remove Window Effect
            Destroy(walls[i].transform.Find("BlobScrollerLeft").gameObject);
            Destroy(walls[i].transform.Find("BlobScrollerRight").gameObject);

            // Replace Windows
            Tilemap tilemap = walls[i].transform.Find("Tilemap").GetComponent<Tilemap>();

            BoundsInt bounds = tilemap.cellBounds;
            TileBase[] allTiles = tilemap.GetTilesBlock(bounds);

            for (int x = 0; x < bounds.size.x; x++)
            {
                for (int y = 0; y < bounds.size.y; y++)
                {
                    TileBase currentTile = allTiles[x + y * bounds.size.x];

                    if (!originalTiles.Contains(currentTile)) continue;
                       
                    tilemap.SetTile(new Vector3Int(x + bounds.x, y + bounds.y, 0), replacementTile);
                }
            }
        }

        // Assign keys
        RoomKeyManager keyManager = GetComponent<RoomKeyManager>();
        for (int i = 0;i < 3; i++)
        {
            keyManager.keys[i] = r.keys[i];
        }

        // Teleport player into room and deactivate mobs for that room
        if (r.isStartRoom)
        {
            GameObject.Find("Player").transform.position = transform.position;
            GetComponent<MonsterSpawner>().isCharged = false;

            // Titles
            GameManager gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
            GameObject titlePrefab = PrefabManager.GetPrefabs().title;
            GameObject title = Instantiate(titlePrefab);
            title.transform.parent = transform;
            title.transform.localPosition = Vector3.zero;
            title.GetComponent<FloorTitle>().Setup(gameManager.GetLevel());
        }

        // Spawn exit hatch
        if (r.isEndRoom)
        {
            GameObject exitHatch = Instantiate(PrefabManager.GetPrefabs().exitHatch);
            exitHatch.transform.parent = transform;
            exitHatch.transform.localPosition = Vector3.zero;
            exitHatch.GetComponent<Hatch>().Setup();
        }
    }

    public void LinkDoors(GameObject[] rooms)
    {
        // Link doors
        for (int i = 0; i < 4; i++)
        {
            if ((int)r.doors[i, 1] == -1) // Skip direction if no door there
                continue;
            int linkedRoomID = ((Room)r.doors[i, 0]).GetID();
            RoomGenerator linkedRoom = rooms[linkedRoomID].GetComponent<RoomGenerator>();
            doors[i].GetComponent<DoorOperation>().counterPart = linkedRoom.doors[(i + 2) % 4];
        }
    }

    public bool IsRoomBehindWall(int wall)
    {
        Vector2Int wallDirection = new Vector2Int((wall == 1) ? 1 : (wall == 3) ? -1 : 0, (wall == 0) ? 1 : (wall == 2) ? -1 : 0);
        //Debug.Log("Room Pos: " + r.position);
        //Debug.Log("wall id: " + wall + " -> " + wallDirection);
        Vector2Int potentialRoom = r.position + wallDirection;
        return potentialRoom == new Vector2Int(Mathf.Clamp(potentialRoom.x, 0, 2), Mathf.Clamp(potentialRoom.y, 0, 2));
    }
    private Color brightenColor(Color color)
    {
        float Brighten(float x)
        {
            return Mathf.Clamp01(Mathf.Sqrt(x + 0.1f));
        }
        return new Color(Brighten(color.r), Brighten(color.g), Brighten(color.b), color.a);
    }

    private Color GetLevelColor(int level)
    {
        switch (level)
        {
            case 0:
                return new Color(0.792f, 0.490f, 0.349f);
            case 1:
                return new Color(0.702f, 0.702f, 0.702f);
            case 2:
                return new Color(0.847f, 0.733f, 0.302f);
        }
        return new Color(0f, 0f, 0f, 0f);
    }
}
