using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageGenerator : MonoBehaviour
{
    private Room[] roomLayout;
    private GameObject[] rooms = new GameObject[9];
    [SerializeField]
    private GameObject roomPrefab;

    // Start is called before the first frame update
    void Start()
    {
        roomLayout = GameObject.Find("StageManager").GetComponent<StageRoomLayout>().GenerateRoomLayout();
        
        // Spawn rooms in 3 by 3 grid
        foreach (Room roomBlueprint in roomLayout)
        {
            Vector3 roomPosition = new Vector3(roomBlueprint.position.x * 10 * 2, roomBlueprint.position.y * 10 * 2, 0f);
            rooms[roomBlueprint.getID()] = Instantiate(roomPrefab, roomPosition, transform.rotation);
            RoomGenerator room = rooms[roomBlueprint.getID()].GetComponent<RoomGenerator>();
            room.Setup(roomBlueprint);
        }
        foreach (GameObject r in rooms)
        {
            RoomGenerator room = r.GetComponent<RoomGenerator>();
            room.LinkDoors(rooms);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
