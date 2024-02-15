using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MonsterBehaviour : MonoBehaviour
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
    [SerializeField] public bool isTouchDamage;
    [SerializeField] public float touchCooldown;
    [SerializeField] public bool isSpreading;
    [SerializeField] public float spreadingCooldown;
    [SerializeField] public int maxSpreadDensity;
    [SerializeField] public float damage;
    [SerializeField] public float health;
    [SerializeField] public int reward;
    [SerializeField] public Sprite appearance;

    private bool isDead = false;
    private float lastShot = 0;
    private float lastStab = 0;
    private float lastTouch = 0;
    private float lastSpread = 0;
    private GameObject stabber;

    private float dyingProgress = -1f;
    private float dyingTime = 0.3f;
    private float[] sizeValues = new float[2];
    private float[] opacityValues = new float[2] { 1, 0 };

    private GameObject room;
    private Loader loader;
    private Transform player;
    private MonsterMovement movement;
    private SpriteRenderer look;

    private Color damageIndicationColor = new Color(1f, 0.5f, 0.5f);
    private float damageIndicationStrength = 0f;
    private float damageIndicationDuration = 0.4f;

    private bool hasNapalm = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void Setup()
    {
        loader = GameObject.Find("StageManager").GetComponent<Loader>();
        int roomID = loader.getRoomIDClosestToPosition(transform.position);
        room = GameObject.Find("StageManager").GetComponent<StageGenerator>().rooms[roomID];
        player = GameObject.Find("Player").transform;
        movement = GetComponent<MonsterMovement>();
        look = transform.Find("Appearance").GetComponent<SpriteRenderer>();
        look.sprite = appearance;

        if (!isStabbing) return;
        stabber = Instantiate(stabbingPrefab);
        stabber.transform.parent = transform;
        //stabber.transform.localPosition = new Vector3(0.0f, (monsterType == 2) ? -0.3f : -0.1f, 0.0f);
        stabber.transform.localPosition = Vector3.zero;
        stabber.GetComponent<StabberBehaviour>().damage = damage;
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead) return;

        if (dyingProgress >= 0)
        {
            look.color = new Color(1f, 1f, 1f, Mathf.Lerp(opacityValues[0], opacityValues[1], dyingProgress));
            transform.localScale = Vector3.one * Mathf.Lerp(sizeValues[0], sizeValues[1], dyingProgress);
            
            dyingProgress += Time.deltaTime / dyingTime;

            if (dyingProgress > 1f)
            {
                Destroy(gameObject);
            }
        }

        if (damageIndicationStrength > 0)
        {
            damageIndicationStrength = Mathf.Clamp01(damageIndicationStrength - Time.deltaTime / damageIndicationDuration);
            look.GetComponent<SpriteRenderer>().color = Color.Lerp(Color.white, damageIndicationColor, damageIndicationStrength);
        }

        if (isShootingProjectile)
        {
            if (Time.time - lastShot < shootingCooldown) goto FailedShooting;
            lastShot  = Time.time;
            ProjectileBehaviour projectile = Instantiate(projectilePrefab, transform.position, transform.rotation).GetComponent<ProjectileBehaviour>();
            projectile.damage = damage;
            projectile.isMonster = projectilePrefab.name.Equals("Goo");
            Vector3 playerDirection = (player.position - transform.position).normalized;
            projectile.Shoot(playerDirection);
        }
        FailedShooting:

        if (isStabbing)
        {
            if ((transform.position - player.position).magnitude >= movement.range) goto FailedStabbing;
            if (Time.time - lastStab < stabbingCooldown) goto FailedStabbing;
            lastStab = Time.time;
            stabber.GetComponent<StabberBehaviour>().Stab();
        }
        FailedStabbing:

        if (isTouchDamage)
        {
            if ((transform.position - player.position).magnitude >= movement.range) goto FailedTouching;
            if (Time.time - lastTouch < stabbingCooldown) goto FailedTouching;
            lastTouch = Time.time;
            player.GetComponent<PlayerBehaviour>().TakeDamage(damage);
        }
        FailedTouching:

        if (isSpreading)
        {
            if (Time.time - lastSpread < spreadingCooldown) goto FailedSpreading;
            // Test random clone position
            Vector3 randomPosition = transform.position + (Vector3) GetRandomUnitVector2D();
            GameObject[] currentMonsters = GameObject.FindGameObjectsWithTag("Monster");
            
            // Calculate monster density at random point
            int density = 0;
            foreach (GameObject monster in currentMonsters)
            {
                if ((monster.transform.position - randomPosition).magnitude > 1)
                    continue;
                density++;
                if (density > maxSpreadDensity) goto FailedSpreading;
            }

            // Spawn clone at random point
            GameObject.Find("StageManager").GetComponent<MonsterTable>().instantiateMonster(2, 0, randomPosition);
            room.GetComponent<MonsterSpawner>().createdMonster(0);
            lastSpread = Time.time;
        }
        FailedSpreading:
        return;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isPassthroughDamage) return;
        if (!collision.gameObject.name.Equals("Player")) return;
        player.GetComponent<PlayerBehaviour>().TakeDamage(damage);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!isBurstingOnRange) return;
        if (!collision.gameObject.name.Equals("Player")) return;
        if ((transform.position - collision.transform.position).magnitude >= movement.range) return;
        // Is in range to explode -> Die, damage Player
        health = 0;
        Dying(2.5f);
        SpawnReward(reward);
        LastMonsterCheck();
        player.GetComponent<PlayerBehaviour>().TakeDamage(damage);
        GetComponent<CircleCollider2D>().enabled = false;
    }


    public void takeDamage(float damage)
    {
        if (health <= 0) return;
        //Debug.Log($"Actually took damage {damage}?");
        health -= damage;
        DamageIndication();

        if (health <= 0)
        {
            Dying(0.0f);
            LastMonsterCheck();
            SpawnReward(reward);
        }
    }

    private void LastMonsterCheck()
    {
        if (room.GetComponent<MonsterSpawner>().isLastMonsterKilled(monsterType))
        {
            SpawnKeys(room.GetComponent<RoomKeyManager>().getKeysToDrop());
            room.GetComponent<MonsterSpawner>().isActive = false;
        }
    }

    private void SpawnKeys(int[] keyAmounts)
    {
        Item.SpawnItems("BronzeKey", keyAmounts[0], transform.position);
        Item.SpawnItems("SilverKey", keyAmounts[1], transform.position);
        Item.SpawnItems("GoldKey", keyAmounts[2], transform.position);
    }

    private void SpawnReward(int amount)
    {
        //Debug.Log($"Spawn reward: {amount}");
        Item.SpawnItems("Currency", amount, transform.position);
    }

    private void Dying(float scaleTargetFaktor)
    {
        // Initiate dying (fade transparent and scale to given scale, delete self at end)
        // Watch out for children
        dyingProgress = 0f;
        sizeValues[0] = transform.localScale.x;
        sizeValues[1] = sizeValues[0] * scaleTargetFaktor;
    }

    private void DamageIndication()
    {
        // Red glow or something
        damageIndicationStrength = 1f;
    }

    public void AddNapalmParticles()
    {
        if (hasNapalm) return;
        GameObject napalmParticlesPrefab = GameObject.Find("NapalmParticles");
        GameObject napalmParticles = Instantiate(PrefabManager.GetPrefabs().napalmParticles);
        napalmParticles.transform.parent = transform;
        napalmParticles.transform.localPosition = Vector3.zero;
        napalmParticles.transform.localScale = Vector3.one;
        hasNapalm = true;
    }

    private Vector2 GetRandomUnitVector2D()
    {
        float randomAngle = Random.Range(0f, 2f * Mathf.PI);
        float x = Mathf.Cos(randomAngle);
        float y = Mathf.Sin(randomAngle);
        return new Vector2(x, y);
    }
}
