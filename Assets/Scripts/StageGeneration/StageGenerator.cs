using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageGenerator : MonoBehaviour
{
    private Room[] roomLayout;
    public GameObject[] rooms = new GameObject[9];

    public void GenerateStage(int level)
    {
        roomLayout = GameObject.Find("StageManager").GetComponent<StageRoomLayout>().GenerateRoomLayout();

        // Spawn rooms in 3 by 3 grid
        foreach (Room roomBlueprint in roomLayout)
        {
            Vector3 roomPosition = new Vector3(roomBlueprint.position.x * 10 * 2, roomBlueprint.position.y * 10 * 2, 0f);
            rooms[roomBlueprint.GetID()] = Instantiate(PrefabManager.GetPrefabs().roomPrefab, roomPosition, transform.rotation);
            RoomGenerator room = rooms[roomBlueprint.GetID()].GetComponent<RoomGenerator>();
            room.Setup(roomBlueprint, level);
        }
        foreach (GameObject r in rooms)
        {
            RoomGenerator room = r.GetComponent<RoomGenerator>();
            room.LinkDoors(rooms);
        }
    }
}
