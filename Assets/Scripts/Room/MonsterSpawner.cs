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
        roomLevel = GetComponent<RoomGenerator>().roomLevel;
        monsterTable = GameObject.Find("StageManager").GetComponent<MonsterTable>();
    }

    public void spawnMonsters()
    {
        if (!isCharged)
            return;

        isCharged = false;
        isActive = true;
        int[] monsterAmounts = monsterTable.getMonsterAmounts(GameObject.Find("GameManager").GetComponent<GameManager>().GetLevel(), roomLevel);
        monstersLeft = (int[]) monsterAmounts.Clone();

        for (int i = 0; i < monsterAmounts.Length; i++)
        {
            for (int j = 0; j < monsterAmounts[i]; j++)
            {
                Vector3 randomPosition = transform.position + new Vector3(Random.Range(-2f, 2f), Random.Range(-2f, 2f), 0f);
                monsterTable.instantiateMonster(GameObject.Find("GameManager").GetComponent<GameManager>().GetLevel(), i, randomPosition);
            }
        }
    }

    public void createdMonster(int monsterType)
    {
        monstersLeft[monsterType]++;
    }

    public bool isLastMonsterKilled(int monsterType)
    {
        monstersLeft[monsterType]--;

        if (monstersLeft.Sum() > 0)
            return false;

        return true;
    }
}
