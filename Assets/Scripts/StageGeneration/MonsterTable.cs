using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterTable : MonoBehaviour
{
    [SerializeField] private GameObject bubblePrefab;
    [SerializeField] private GameObject tentaclePrefab;
    [SerializeField] private GameObject tinyTentaclePrefab;
    [SerializeField] private GameObject harpunePrefab;
    [SerializeField] private GameObject gooPrefab;
    
    [SerializeField] private Sprite[] appearances = new Sprite[9];

    [SerializeField] private GameObject monsterBase;

    public int[] getMonsterAmounts(int stage, int roomLevel)
    {
        int[] amounts = new int[3];
        switch (stage)
        {
            case 2:
                switch (roomLevel)
                {
                    case 0:
                        amounts[0] = monsterRange(5, 3);
                        amounts[1] = monsterRange(0, 1);
                        amounts[2] = monsterRange(0, 0);
                        break;
                    case 1:
                        amounts[0] = monsterRange(4, 2);
                        amounts[1] = monsterRange(3, 2);
                        amounts[2] = monsterRange(0, 0);
                        break;
                    case 2:
                        amounts[0] = monsterRange(0, 0);
                        amounts[1] = monsterRange(0, 0);
                        amounts[2] = monsterRange(1, 0);
                        break;
                }
                break;
            case 1:
                switch (roomLevel)
                {
                    case 0:
                        amounts[0] = monsterRange(8, 2);
                        amounts[1] = monsterRange(0, 1);
                        amounts[2] = monsterRange(0, 0);
                        break;
                    case 1:
                        amounts[0] = monsterRange(5, 1);
                        amounts[1] = monsterRange(4, 1);
                        amounts[2] = monsterRange(0, 0);
                        break;
                    case 2:
                        amounts[0] = monsterRange(0, 0);
                        amounts[1] = monsterRange(0, 0);
                        amounts[2] = monsterRange(1, 0);
                        break;
                }
                break;
            case 0:
                switch (roomLevel)
                {
                    case 0:
                        amounts[0] = monsterRange(15, 5);
                        amounts[1] = monsterRange(0, 2);
                        amounts[2] = monsterRange(0, 0);
                        break;
                    case 1:
                        amounts[0] = monsterRange(5, 5);
                        amounts[1] = monsterRange(3, 1);
                        amounts[2] = monsterRange(0, 0);
                        break;
                    case 2:
                        amounts[0] = monsterRange(0, 0);
                        amounts[1] = monsterRange(2, 1);
                        amounts[2] = monsterRange(1, 0);
                        break;
                }
                break;
        }
        int monsterRange(int monsterAmount, int randomRange)
        {
            return monsterAmount + (int) Mathf.Abs(Random.Range(-randomRange, randomRange + 1.0f));
        }
        
        return amounts;
    }

    public GameObject instantiateMonster(int level, int monsterID, Vector3 position)
    {
        GameObject monster = null;

        switch (level)
        {
        case 2:
            switch (monsterID)
            {
            case 0: // Pufferfish
                monster = Instantiate(monsterBase, position, Quaternion.Euler(0f, 0f, 0f));
                declareMonsterValues(monster,
                    true, false, 1.5f, 1.0f, 4f, 0.6f, true, 0.4f, 2.5f, false, 1.4f, 0.5f, false, 1300.0f, 3.5f, 2.5f,
                    0, true, false, 0.6f, null, false, 0.5f, null, false, false, 0.35f, false, 0.75f, 8, 15.0f, 30.0f, 1, appearances[0]);
                break;
            case 1: // Blobfish
                monster = Instantiate(monsterBase, position, Quaternion.Euler(0f, 0f, 0f));
                declareMonsterValues(monster,
                    false, true, 3.0f, 1.0f, 0.5f, 6.5f, false, 0.4f, 2.5f, true, 1.2f, 0.25f, false, 1300.0f, 3.5f, 2.5f,
                    1, false, true, 0.6f, bubblePrefab, false, 0.5f, null, false, false, 0.35f, false, 0.75f, 8, 25.0f, 65.0f, 2, appearances[1]);
                break;
            case 2: // Giant glow squid
                monster = Instantiate(monsterBase, position, Quaternion.Euler(0f, 0f, 0f));
                declareMonsterValues(monster,
                    false, false, 1.5f, 1.0f, 0.1f, 10.0f, false, 0.4f, 2.5f, false, 1.4f, 0.5f, false, 1300.0f, 3.5f, 2.5f,
                    2, false, false, 0.6f, null, true, 0.75f, tentaclePrefab, false, false, 0.35f, false, 0.75f, 8, 45.0f, 300.0f, 25, appearances[2]);
                break;
            }
            break; 
        case 1:
            switch (monsterID)
            {
                case 0: // Glowing squid
                    monster = Instantiate(monsterBase, position, Quaternion.Euler(0f, 0f, 0f));
                    declareMonsterValues(monster,
                        true, false, 5.0f, 3.0f, 0.25f, 0.5f, false, 0.4f, 2.5f, true, 1.4f, 1.5f, false, 1300.0f, 3.5f, 2.5f,
                        0, false, false, 0.6f, null, true, 0.5f, tinyTentaclePrefab, false, false, 0.35f, false, 0.75f, 8, 25.0f, 45.0f, 3, appearances[3]);
                    break;
                case 1: // Mutated shark
                    monster = Instantiate(monsterBase, position, Quaternion.Euler(0f, 0f, 0f));
                    declareMonsterValues(monster,
                        false, false, 3.0f, 1.0f, 0.5f, 6.5f, false, 0.4f, 2.5f, false, 1.2f, 0.25f, true, 1300.0f, 3.5f, 2.5f,
                        1, false, false, 0.6f, null, false, 0.5f, null, true, false, 0.35f, false, 0.75f, 8, 30.0f, 90.0f, 6, appearances[4]);
                    break;
                case 2: // Giant angler fish
                    monster = Instantiate(monsterBase, position, Quaternion.Euler(0f, 0f, 0f));
                    declareMonsterValues(monster,
                        true, false, 1.5f, 1.0f, 0.1f, 1.0f, true, 0.75f, 0.75f, false, 1.4f, 0.5f, false, 1300.0f, 3.5f, 2.5f,
                        2, false, false, 0.6f, null, true, 1.0f, null, false, false, 0.35f, false, 0.75f, 8, 100.0f, 300.0f, 60, appearances[5]);
                    break;
            }
            break;
        case 0:
            switch (monsterID)
            {
                case 0: // Goo
                    monster = Instantiate(monsterBase, position, Quaternion.Euler(0f, 0f, 0f));
                    declareMonsterValues(monster,
                        false, false, 1.5f, 1.0f, 0.1f, 0.0f, false, 0.4f, 2.5f, true, 1.2f, 0.65f, false, 1300.0f, 3.5f, 2.5f,
                        0, false, false, 0.6f, null, false, 0.5f, null, false, true, 0.35f, true, 0.5f, 8, 5.0f, 50.0f, 1, appearances[6]);
                    break;
                case 1: // Infected diver
                    monster = Instantiate(monsterBase, position, Quaternion.Euler(0f, 0f, 0f));
                    declareMonsterValues(monster,
                        true, false, 7.0f, 2.0f, 0.5f, 4.0f, false, 0.4f, 2.5f, false, 1.2f, 0.25f, false, 1300.0f, 3.5f, 2.5f,
                        1, false, true, 1.2f, harpunePrefab, false, 0.5f, null, false, false, 0.35f, false, 0.75f, 8, 50.0f, 100.0f, 10, appearances[7]);
                    break;
                case 2: // Giant goo Monster
                    monster = Instantiate(monsterBase, position, Quaternion.Euler(0f, 0f, 0f));
                    declareMonsterValues(monster,
                        false, false, 1.5f, 1.0f, 0.1f, 0f, false, 0.4f, 2.5f, true, 1.1f, 0.65f, false, 1300.0f, 3.5f, 2.5f,
                        2, false, true, 0.4f, gooPrefab, false, 0.5f, null, false, true, 0.6f, false, 0.75f, 8, 15.0f, 500.0f, 100, appearances[8]);
                    break;
            }
            break;
        }

        return monster;
    }

    private void declareMonsterValues(GameObject monster,  bool isMovingTowardsPlayer, bool isFleeingFromPlayer, 
        float maxSpeed, float timeToAccelerate, float timeToDecelerate, float range, bool isJumping, float jumpHeight, 
        float jumpSpeed, bool isStretchMoving, float stretchFaktor, float stretchSpeed, bool isDashing, float dashForce, 
        float linearDrag, float dashCooldown,
        int monsterType, bool isBurstingOnRange, bool isShootingProjectile, float shootingCooldown, GameObject projectilePrefab,
        bool isStabbing, float stabbingCooldown, GameObject stabbingPrefab, bool isPassthroughDamage, bool isTouchDamage, 
        float touchCooldown, bool isSpreading, float spreadingCooldown, int maxSpreadDensity,
        float damage, float health, int reward, Sprite appearance)
    {
        MonsterMovement monsterMovement = monster.GetComponent<MonsterMovement>();

        monsterMovement.isMovingTowardsPlayer = isMovingTowardsPlayer;
        monsterMovement.isFleeingFromPlayer = isFleeingFromPlayer;
        monsterMovement.maxSpeed = maxSpeed;
        monsterMovement.timeToAccelerate = timeToAccelerate;
        monsterMovement.timeToDecelerate = timeToDecelerate;
        monsterMovement.range = range;
        monsterMovement.isJumping = isJumping;
        monsterMovement.jumpHeight = jumpHeight;
        monsterMovement.jumpSpeed = jumpSpeed;
        monsterMovement.isStretchMoving = isStretchMoving;
        monsterMovement.stretchFaktor = stretchFaktor;
        monsterMovement.stretchSpeed = stretchSpeed;
        monsterMovement.isDashing = isDashing;
        monsterMovement.dashForce = dashForce;
        monsterMovement.linearDrag = linearDrag;
        monsterMovement.dashCooldown = dashCooldown;
        monsterMovement.Setup();

        MonsterBehaviour monsterBehaviour = monster.GetComponent<MonsterBehaviour>();

        monsterBehaviour.monsterType = monsterType;
        monsterBehaviour.isBurstingOnRange = isBurstingOnRange;
        monsterBehaviour.isShootingProjectile = isShootingProjectile;
        monsterBehaviour.shootingCooldown = shootingCooldown;
        monsterBehaviour.projectilePrefab = projectilePrefab;
        monsterBehaviour.isStabbing = isStabbing;
        monsterBehaviour.stabbingCooldown = stabbingCooldown;
        monsterBehaviour.stabbingPrefab = stabbingPrefab;
        monsterBehaviour.isPassthroughDamage = isPassthroughDamage;
        monsterBehaviour.isTouchDamage = isTouchDamage;
        monsterBehaviour.touchCooldown = touchCooldown;
        monsterBehaviour.isSpreading = isSpreading;
        monsterBehaviour.spreadingCooldown = spreadingCooldown;
        monsterBehaviour.maxSpreadDensity = maxSpreadDensity;
        monsterBehaviour.damage = damage;
        monsterBehaviour.health = health;
        monsterBehaviour.reward = reward;
        monsterBehaviour.appearance = appearance;
        monsterBehaviour.Setup();
    }
}
