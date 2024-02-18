using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource doorOpen;
    public AudioSource doorMissingKey;
    public AudioSource playerHit;
    public AudioSource itemPickup;
    public AudioSource hatchInteract;
    public AudioSource monsterHit;
    public AudioSource monsterDie;

    // Start is called before the first frame update
    void Start()
    {
        doorOpen = transform.Find("DoorOpen").GetComponent<AudioSource>();
        doorMissingKey = transform.Find("DoorMissingKey").GetComponent<AudioSource>();
        playerHit = transform.Find("PlayerHit").GetComponent<AudioSource>();
        itemPickup = transform.Find("ItemPickup").GetComponent<AudioSource>();
        hatchInteract = transform.Find("HatchInteract").GetComponent<AudioSource>();
        monsterDie = transform.Find("MonsterDie").GetComponent<AudioSource>();
    }
}
