using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBehavior : MonoBehaviour
{
    [SerializeField] public int monsterType;
    [SerializeField] public bool isBurstingOnRange;
    [SerializeField] public bool isShootingProjectile;
    [SerializeField] public float shootingCooldown;
    [SerializeField] public GameObject projectilePrefab;
    [SerializeField] public bool isStabbing;
    [SerializeField] public float stabbingCooldown;
    [SerializeField] public GameObject stabbingPrefab;
    [SerializeField] public bool isPassthroughDamage;
    [SerializeField] public bool isSpreading;
    [SerializeField] public float spreadingCooldown;
    [SerializeField] public float maxSpreadDensity;
    [SerializeField] public float damage;
    [SerializeField] public float health = 1.0f;
    private GameObject room;
    private Loader loader;

    // Start is called before the first frame update
    void Start()
    {
        loader = GameObject.Find("StageManager").GetComponent<Loader>();
        int roomID = loader.getRoomIDClosestToPosition(transform.position);
        room = GameObject.Find("StageManager").GetComponent<StageGenerator>().rooms[roomID];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void takeDamage(float damage)
    {
        health -= damage;
        // Damage indicator

        if (health < 0)
        {
            // Start dying
            if (room.GetComponent<MonsterSpawner>().isLastMonsterKilled(monsterType))
            {
                // Drop loot
            }
        }
    }
}
