using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOperation : MonoBehaviour
{
    public GameObject counterPart;
    private bool isUnlocked = false;
    private Transform player;
    private Loader loader;
    private int roomID;
    private float interactionDistance = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").transform;
        loader = GameObject.Find("StageManager").GetComponent<Loader>();
        roomID = transform.parent.parent.GetComponent<RoomGenerator>().roomID;
    }

    // Update is called once per frame
    void Update()
    {
        if (loader.currentRoomID == roomID)
        {
            float playerDistance = (transform.position - player.position).magnitude;
            if (playerDistance < interactionDistance)
            {
                interactionCheck();
            }
        }
    }

    private void interactionCheck()
    {
        Debug.Log("Awaiting E...");
        if (!Input.GetKeyUp(KeyCode.E))
            return;
        if (!isUnlocked)
        {
            // Check if enough keys of type collected
            
            // Open door and counterpart
            isUnlocked = true;
            counterPart.GetComponent<DoorOperation>().isUnlocked = true;

            // Signal Error otherwise
            return;
        }
        counterPart.GetComponent<DoorOperation>().teleportPlayer();
    }

    public void teleportPlayer()
    {
        Vector2 roomCenterDirection = (transform.parent.parent.position - transform.position).normalized;
        Vector2 targetPosition = (Vector2)transform.position + roomCenterDirection * 1.25f;
        player.transform.position = targetPosition;
    }
}
