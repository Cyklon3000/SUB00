using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOperation : MonoBehaviour
{
    public GameObject counterPart;
    private bool isUnlocked = false;
    private Transform player;
    private Loader loader;
    private GameObject room;
    private int roomID;
    public int doorLevel;
    private float interactionDistance = 1.5f;
    MonsterSpawner monsterSpawner;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").transform;
        loader = GameObject.Find("StageManager").GetComponent<Loader>();
        room = transform.parent.parent.gameObject;
        monsterSpawner = room.GetComponent<MonsterSpawner>();
        roomID = room.GetComponent<RoomGenerator>().roomID;
    }

    // Update is called once per frame
    void Update()
    {
        if (loader.currentRoomID == roomID)
        {
            if (monsterSpawner.isActive)
                return;
            
            float playerDistance = (transform.position - player.position).magnitude;
            if (playerDistance < interactionDistance)
            {
                interactionCheck();
                if (isUnlocked)
                {
                    GetComponent<SpriteRenderer>().sprite = GameObject.Find("PrefabCollector").GetComponent<PrefabManager>().openDoor;
                    return;
                }
            }
            GetComponent<SpriteRenderer>().sprite = GameObject.Find("PrefabCollector").GetComponent<PrefabManager>().closedDoor;
        }
    }

    private void interactionCheck()
    {
        //Debug.Log("Awaiting E...");
        if (!Input.GetKeyUp(KeyCode.E))
            return;
        if (!isUnlocked)
        {
            // Check if enough keys of type collected
            string keyName = Inventory.IDtoItemName(doorLevel);
            if (player.GetComponent<Inventory>().payItems(keyName, 1))
            {
                // Open door and counterpart
                isUnlocked = true;
                counterPart.GetComponent<DoorOperation>().isUnlocked = true;
            }
            else
            {
                // Signal Error otherwise
                Debug.Log("No fitting key!");
            }
            return;
        }
        counterPart.GetComponent<DoorOperation>().teleportPlayer();
    }

    public void teleportPlayer()
    {
        // Attempt spawning monsters
        monsterSpawner.spawnMonsters();

        Vector2 roomCenterDirection = (transform.parent.parent.position - transform.position).normalized;
        Vector2 targetPosition = (Vector2)transform.position + roomCenterDirection * 1.25f;
        player.transform.position = targetPosition;
    }
}
