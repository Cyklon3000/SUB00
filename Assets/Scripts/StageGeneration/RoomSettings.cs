using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSettings : MonoBehaviour
{
    public MonsterTable[] roomMonsters = new MonsterTable[3]; // Monster Tables for 3 different room levels

    void Start()
    {
        // bronze room                     0 0r  1 1r  2 2r
        roomMonsters[0] = new MonsterTable(5, 3, 0, 1, 0, 0);
        // silver room
        roomMonsters[0] = new MonsterTable(4, 2, 3, 2, 0, 0);
        // golden room
        roomMonsters[0] = new MonsterTable(0, 0, 0, 0, 1, 0);
    }
}
