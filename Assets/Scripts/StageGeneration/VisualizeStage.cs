using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class VisualizeStage : MonoBehaviour
{
    private StageRoomLayout layout;
    private Room[] rooms;
    private int[] doorAmount;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        layout = GetComponent<StageRoomLayout>();
        rooms = layout.latestRooms;
        doorAmount = layout.doorAmounts;
    }

    private void OnDrawGizmos()
    {
        foreach (Room room in this.rooms)
        {
            if (room.roomLevel == 0)
                Gizmos.color = new Color(1f, 0.25f, 0f);
            if (room.roomLevel == 1)
                Gizmos.color = Color.gray;
            if (room.roomLevel == 2)
                Gizmos.color = Color.yellow;
            if (room.isStartRoom)
                Gizmos.color = Color.green;
            if (room.isEndRoom)
                Gizmos.color = Color.red;

            // Calculate the four corners of the rectangle
            Vector3 topLeft = new Vector3(room.position.x, room.position.y + .9f, 0f);
            Vector3 topRight = new Vector3(room.position.x + .9f, room.position.y + .9f, 0f);
            Vector3 bottomLeft = new Vector3(room.position.x, room.position.y, 0f);
            Vector3 bottomRight = new Vector3(room.position.x + .9f, room.position.y, 0f);

            // Draw lines to form the rectangle
            Gizmos.DrawLine(topLeft, topRight);
            Gizmos.DrawLine(topRight, bottomRight);
            Gizmos.DrawLine(bottomRight, bottomLeft);
            Gizmos.DrawLine(bottomLeft, topLeft);

            for (int i = 0; i < 4; i++)
            {
                switch ((int)room.doors[i, 1])
                {
                    case -1:
                        continue;
                    case 0:
                        Gizmos.color = new Color(1f, 0.25f, 0f);
                        break;
                    case 1:
                        Gizmos.color = Color.gray;
                        break;
                    case 2:
                        Gizmos.color = Color.yellow;
                        break;
                }

                Vector2 centreOffset = Vector2.zero;
                switch (i)
                {
                    case 0:
                        centreOffset = new Vector2(0.45f, 0.9f);
                        break;
                    case 1:
                        centreOffset = new Vector2(0.9f, 0.45f);
                        break;
                    case 2:
                        centreOffset = new Vector2(0.45f, 0f);
                        break;
                    case 3:
                        centreOffset = new Vector2(0, 0.45f);
                        break;
                }
                Vector3 xCenter = new Vector3(room.position.x + centreOffset.x, room.position.y + centreOffset.y, 0f);
                Gizmos.DrawLine(xCenter - (Vector3)Vector2.one * 0.05f, xCenter + (Vector3)Vector2.one * 0.05f);
            }

            for (int i = 0; i < 3; i++)
            {
                Vector3 basePos = new Vector3(room.position.x, room.position.y, 0f);
                if (room.keys[0] == 1)
                {
                    Gizmos.color = new Color(1f, 0.25f, 0f);
                    Gizmos.DrawLine(basePos - (Vector3)Vector2.one * 0.05f, basePos + (Vector3)Vector2.one * 0.05f);
                    basePos += new Vector3(0.05f, 0f, 0f);
                }
                if (room.keys[1] == 1)
                {
                    Gizmos.color = Color.gray;
                    Gizmos.DrawLine(basePos - (Vector3)Vector2.one * 0.05f, basePos + (Vector3)Vector2.one * 0.05f);
                    basePos += new Vector3(0.05f, 0f, 0f);
                }
                if (room.keys[2] == 1)
                {
                    Gizmos.color = Color.yellow;
                    Gizmos.DrawLine(basePos - (Vector3)Vector2.one * 0.05f, basePos + (Vector3)Vector2.one * 0.05f);
                }
            }
        }
    }
}
