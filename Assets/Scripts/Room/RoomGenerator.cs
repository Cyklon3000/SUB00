using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomGenerator : MonoBehaviour
{
    public Room r;
    public GameObject floor;
    public GameObject[] walls = new GameObject[4];
    public GameObject[] doors = new GameObject[4];
    
    // Start is called before the first frame update
    void Awake()
    {
        // Get references
        floor = transform.Find("Floor").gameObject;
        Transform wallParent = transform.Find("Walls");
        Transform doorParent = transform.Find("Doors");
        string[] directions = new string[4] { "Up", "Right", "Down", "Left" };
        for (int i = 0; i < 4; i++)
        {
            walls[i] = wallParent.Find($"Wall{directions[i]}").gameObject;
            doors[i] = doorParent.Find($"Door{directions[i]}").gameObject;
        }

        // Colorcode the Room Elements according to level
        floor.GetComponent<SpriteRenderer>().color = getLevelColor(r.roomLevel);
        for (int i = 0; i < 4; i++)
        {
            doors[i].GetComponent<SpriteRenderer>().color = getLevelColor((int) r.doors[i, 1]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private Color getLevelColor(int level)
    {
        switch (level)
        {
            case 0:
                return Gizmos.color = new Color(1f, 0.25f, 0f);
            case 1:
                return Color.gray;
            case 2:
                return Color.yellow;
        }
        return Color.white;
    }
}
