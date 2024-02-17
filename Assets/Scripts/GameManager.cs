using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Current level starting at 2, then 1, then 0
    private int level = 3;

    // Start is called before the first frame update
    void Start()
    {
        LoadNextLevel(false);
    }

    public int GetLevel()
    {
        return level;
    }

    public void LoadNextLevel(bool fullRoutine = true)
    {
        level--;
        Debug.Log($"Loading Level {level}");
        if (level < 0)
        {
            Debug.Log("GAME COMPLETE!!!");
            DeleteCurrentLevel();
            return;
        }
        DeleteCurrentLevel();
        StageGenerator stageGenerator = GameObject.Find("StageManager").GetComponent<StageGenerator>();
        stageGenerator.GenerateStage(level);
        GameObject.Find("Player").GetComponent<PlayerBehaviour>().ResetHealth();
        if (!fullRoutine) return;
        GameObject.Find("Player").GetComponent<Inventory>().clearKeys();
        GameObject.Find("Player").GetComponent<Inventory>().setItems("BronzeKey", 1);

        GameObject.Find("Player").GetComponent<Inventory>().setItems("GoldKey", 1);
    }

    private void DeleteCurrentLevel()
    {
        GameObject[] monsters = GameObject.FindGameObjectsWithTag("Monster");
        foreach (GameObject monster in monsters)
        {
            Destroy(monster);
        }
        GameObject[] rooms = GameObject.FindGameObjectsWithTag("Room");
        foreach (GameObject room in rooms)
        {
            Destroy(room);
        }
        GameObject[] items = GameObject.FindGameObjectsWithTag("Item");
        foreach (GameObject room in items)
        {
            Destroy(room);
        }
    }
}
