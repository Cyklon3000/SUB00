using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

public class Loader : MonoBehaviour
{
    public int currentRoomID = -1;
    private Transform player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        // Get current room
        currentRoomID = getRoomIDClosestToPosition(player.position);
    }

    public int getRoomIDClosestToPosition(Vector3 position)
    {
        int closestRoomID = 0;
        float shortestRoomDistanceSqr = Mathf.Infinity;
        foreach (GameObject room in GameObject.FindGameObjectsWithTag("Room"))
        {
            float distanceSqr = (position - room.transform.position).sqrMagnitude;
            if (distanceSqr < shortestRoomDistanceSqr)
            {
                shortestRoomDistanceSqr = distanceSqr;
                closestRoomID = room.GetComponent<RoomGenerator>().roomID;
            }
        }
        return closestRoomID;
    }
}
