using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hatch : MonoBehaviour
{
    private GameObject player;
    private Loader loader;

    private int hatchRoomID;

    private float interactionDistance = 1.5f;
    
    // Start is called before the first frame update
    public void Setup()
    {
        player = GameObject.Find("Player");
        loader = GameObject.Find("StageManager").GetComponent<Loader>();
        hatchRoomID = loader.getRoomIDClosestToPosition(transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        if (hatchRoomID != loader.currentRoomID) return;
        if ((player.transform.position - transform.position).magnitude > interactionDistance) return;
        // Interaction possible
        if (!Input.GetKey(KeyCode.E)) return;
        // Interact (show menu)
        if (GameObject.Find("GameManager").GetComponent<GameManager>().GetLevel() > 0)
            GameObject.Find("UpgradeUI").GetComponent<Upgrade>().ShowUpgradeUI();
        else
            GameObject.Find("GameManager").GetComponent<GameManager>().LoadNextLevel();
    }
}
