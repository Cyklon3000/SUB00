using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorTitle : MonoBehaviour
{
    [SerializeField] private Sprite[] sprites = new Sprite[3];

    public void Setup(int currentLevel)
    {
        GetComponent<SpriteRenderer>().sprite = sprites[currentLevel];
    }
}
