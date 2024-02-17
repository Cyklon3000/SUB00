using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

public class Loader : MonoBehaviour
{
    public int currentRoomID = -1;

    // Update is called once per frame
    void Update()
    {
        // Get current room
        Vector3 playerPosition = GameObject.Find("Player").transform.position;
        currentRoomID = getRoomIDClosestToPosition(playerPosition);
    }

    public int getRoomIDClosestToPosition(Vector3 position)
    {
        int closestRoomID = 0;
        float shortestRoomDistanceSqr = Mathf.Infinity;
        foreach (GameObject room in GameObject.FindGameObjectsWithTag("Room"))
        {
            if (!room.name.StartsWith("Room")) continue;

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
