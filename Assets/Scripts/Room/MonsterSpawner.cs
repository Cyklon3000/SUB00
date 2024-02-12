using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    private int roomLevel = -1;

    // Start is called before the first frame update
    void Start()
    {
        roomLevel = GetComponent<RoomGenerator>().r.roomLevel;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
