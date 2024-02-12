using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    private int roomLevel = -1;
    private MonsterTable monsterTable;
    [HideInInspector]
    public bool isCharged = true;
    [HideInInspector]
    public bool isActive = false;
    private int[] monstersLeft;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("getTable", 0.5f);
    }

    private void getTable()
    {
        roomLevel = GetComponent<RoomGenerator>().r.roomLevel;
        monsterTable = GameObject.Find("StageManager").GetComponent<RoomSettings>().roomMonsters[roomLevel];
    }

    public void spawnMonsters()
    {
        if (!isCharged)
            return;

        isCharged = false;
        isActive = true;
        int[] monsterAmounts = monsterTable.getRandomAmounts();
        monstersLeft = (int[]) monsterAmounts.Clone();

        Debug.Log("Spawn Monsters!");
        Debug.Log($"Type 0: {monsterAmounts[0]}; Type 1: {monsterAmounts[1]}; Type 2: {monsterAmounts[2]}");
    }

    public bool isLastMonsterKilled(int monsterType)
    {
        monstersLeft[monsterType]--;

        if (monstersLeft.Sum() > 0)
            return false;

        return true;
    }
}
