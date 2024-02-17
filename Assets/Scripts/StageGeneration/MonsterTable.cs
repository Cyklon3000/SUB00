using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterTable : MonoBehaviour
{    
    [SerializeField] private Sprite[] appearances = new Sprite[9];

    public int[] GetMonsterAmounts(int stage, int roomLevel)
    {
        int[] amounts = new int[3];
        switch (stage)
        {
            case 2:
                switch (roomLevel)
                {
                    case 0:
                        amounts[0] = MonsterRange(5, 2);
                        amounts[1] = MonsterRange(0, 1);
                        amounts[2] = MonsterRange(0, 0);
                        break;
                    case 1:
                        amounts[0] = MonsterRange(3, 1);
                        amounts[1] = MonsterRange(3, 2);
                        amounts[2] = MonsterRange(0, 0);
                        break;
                    case 2:
                        amounts[0] = MonsterRange(0, 0);
                        amounts[1] = MonsterRange(0, 0);
                        amounts[2] = MonsterRange(1, 0);
                        break;
                }
                break;
            case 1:
                switch (roomLevel)
                {
                    case 0:
                        amounts[0] = MonsterRange(4, 1);
                        amounts[1] = MonsterRange(0, 1);
                        amounts[2] = MonsterRange(0, 0);
                        break;
                    case 1:
                        amounts[0] = MonsterRange(2, 1);
                        amounts[1] = MonsterRange(3, 1);
                        amounts[2] = MonsterRange(0, 0);
                        break;
                    case 2:
                        amounts[0] = MonsterRange(0, 0);
                        amounts[1] = MonsterRange(0, 0);
                        amounts[2] = MonsterRange(1, 0);
                        break;
                }
                break;
            case 0:
                switch (roomLevel)
                {
                    case 0:
                        amounts[0] = MonsterRange(15, 5);
                        amounts[1] = MonsterRange(0, 2);
                        amounts[2] = MonsterRange(0, 0);
                        break;
                    case 1:
                        amounts[0] = MonsterRange(5, 5);
                        amounts[1] = MonsterRange(3, 1);
                        amounts[2] = MonsterRange(0, 0);
                        break;
                    case 2:
                        amounts[0] = MonsterRange(0, 0);
                        amounts[1] = MonsterRange(0, 1);
                        amounts[2] = MonsterRange(1, 0);
                        break;
                }
                break;
        }
        int MonsterRange(int monsterAmount, int randomRange)
        {
            return monsterAmount + (int) Mathf.Abs(Random.Range(-randomRange, randomRange + 1.0f));
        }
        
        return amounts;
    }

    public GameObject InstantiateMonster(int level, int monsterID, Vector3 position)
    {
        GameObject monster = null;

        switch (level)
        {
        case 2:
            switch (monsterID)
            {
                case 0: // Pufferfish
                    monster = Instantiate(PrefabManager.GetPrefabs().monsterBase, position, Quaternion.Euler(0f, 0f, 0f));
                    monster.name = "Pufferfish";
                    DeclareMonsterValues(monster,
                        true, false, 2.0f, 1.0f, 4f, 0.4f, true, 0.4f, 2.5f, false, 1.4f, 0.5f, false, 1300.0f, 3.5f, 2.5f,
                        0, true, false, 0.6f, null, false, 0.5f, null, false, false, 0.35f, false, 0.75f, 8, 15.0f, 20.0f, 1, appearances[0]);
                    break;
                case 1: // Blobfish
                    monster = Instantiate(PrefabManager.GetPrefabs().monsterBase, position, Quaternion.Euler(0f, 0f, 0f));
                    monster.name = "Blobfish";
                    DeclareMonsterValues(monster,
                        false, true, 3.0f, 1.0f, 0.5f, 6.5f, false, 0.4f, 2.5f, true, 1.2f, 0.25f, false, 1300.0f, 3.5f, 2.5f,
                        1, false, true, 2f, PrefabManager.GetPrefabs().bubblePrefab, false, 0.5f, null, false, false, 0.35f, false, 0.75f, 8, 15.0f, 10.0f, 2, appearances[1]);
                    break;
                case 2: // Giant glow squid
                    monster = Instantiate(PrefabManager.GetPrefabs().monsterBase, position, Quaternion.Euler(0f, 0f, 0f));
                    monster.name = "Giant glow squid";
                    DeclareMonsterValues(monster,
                        true, false, 3.0f, 20.0f, 0.1f, 3.5f, false, 0.4f, 2.5f, true, 1.15f, 0.75f, false, 1300.0f, 3.5f, 2.5f,
                        2, false, false, 0.6f, null, true, 0.75f, PrefabManager.GetPrefabs().tentaclePrefab, false, false, 0.35f, false, 0.75f, 8, 20.0f, 150.0f, 25, appearances[2]);
                    break;
            }
            break; 
        case 1:
            switch (monsterID)
            {
                case 0: // Glowing squid
                    monster = Instantiate(PrefabManager.GetPrefabs().monsterBase, position, Quaternion.Euler(0f, 0f, 0f));
                        monster.name = "Glowing squid";
                    DeclareMonsterValues(monster,
                        true, false, 3.0f, 3.0f, 0.25f, 1.75f, false, 0.4f, 2.5f, true, 1.4f, 1.5f, false, 1300.0f, 3.5f, 2.5f,
                        0, false, false, 0.6f, null, true, 0.5f, PrefabManager.GetPrefabs().tinyTentaclePrefab, false, false, 0.35f, false, 0.75f, 8, 15.0f, 30.0f, 3, appearances[3]);
                    break;
                case 1: // Mutated shark
                    monster = Instantiate(PrefabManager.GetPrefabs().monsterBase, position, Quaternion.Euler(0f, 0f, 0f));
                    monster.name = "Mutated shark";
                    DeclareMonsterValues(monster,
                        false, false, 3.0f, 1.0f, 0.5f, 6.5f, false, 0.4f, 2.5f, false, 1.2f, 0.25f, true, 1300.0f, 3.5f, 2.5f,
                        1, false, false, 0.6f, null, false, 0.5f, null, true, false, 0.35f, false, 0.75f, 8, 25.0f, 50.0f, 6, appearances[4]);
                    break;
                case 2: // Giant angler fish
                    monster = Instantiate(PrefabManager.GetPrefabs().monsterBase, position, Quaternion.Euler(0f, 0f, 0f));
                    monster.name = "Giant angler fish";
                    DeclareMonsterValues(monster,
                        true, false, 1.5f, 1.0f, 0.1f, 1.0f, true, 0.75f, 0.75f, false, 1.4f, 0.5f, false, 1300.0f, 3.5f, 2.5f,
                        2, false, false, 0.6f, null, true, 1.0f, PrefabManager.GetPrefabs().anglerRodPrefab, false, false, 0.35f, false, 0.75f, 8, 25.0f, 200.0f, 60, appearances[5]);
                    break;
            }
            break;
        case 0:
            switch (monsterID)
            {
                case 0: // Goo
                    monster = Instantiate(PrefabManager.GetPrefabs().monsterBase, position, Quaternion.Euler(0f, 0f, 0f));
                    monster.name = "Goo";
                    DeclareMonsterValues(monster,
                        false, false, 0f, 1.0f, 0.1f, 0.5f, false, 0.4f, 2.5f, true, 1.2f, 0.65f, false, 1300.0f, 3.5f, 2.5f,
                        0, true, false, 0.6f, null, false, 0.5f, null, false, false, 0.35f, true, 2.5f, 4, 5.5f, 6.0f, 0, appearances[6]);
                    break;
                case 1: // Infected diver
                    monster = Instantiate(PrefabManager.GetPrefabs().monsterBase, position, Quaternion.Euler(0f, 0f, 0f));
                    monster.name = "Infected diver";
                    DeclareMonsterValues(monster,
                        true, false, 7.0f, 2.0f, 0.5f, 4.0f, false, 0.4f, 2.5f, false, 1.2f, 0.25f, false, 1300.0f, 3.5f, 2.5f,
                        1, false, true, 1.5f, PrefabManager.GetPrefabs().harpunePrefab, false, 0.5f, null, false, false, 0.35f, false, 0.75f, 8, 25.0f, 70.0f, 10, appearances[7]);
                    break;
                case 2: // Giant goo Monster
                    monster = Instantiate(PrefabManager.GetPrefabs().monsterBase, position, Quaternion.Euler(0f, 0f, 0f));
                    monster.name = "Giant goo Monster";
                    DeclareMonsterValues(monster,
                        false, false, 0.0f, 1.0f, 0.1f, 0f, false, 0.4f, 2.5f, true, 1.1f, 0.65f, false, 1300.0f, 3.5f, 2.5f,
                        2, false, true, 1.6f, PrefabManager.GetPrefabs().gooPrefab, false, 0.5f, null, false, true, 0.6f, false, 0.75f, 8, 10.0f, 250.0f, 250, appearances[8]);
                    break;
            }
            break;
        }

        return monster;
    }

    private void DeclareMonsterValues(GameObject monster, bool isMovingTowardsPlayer, bool isFleeingFromPlayer,
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
        monsterBehaviour.spreadingCooldown = spreadingCooldown * Random.Range(0.75f, 1.333f);
        monsterBehaviour.maxSpreadDensity = maxSpreadDensity;
        monsterBehaviour.damage = damage;
        monsterBehaviour.health = health;
        monsterBehaviour.reward = reward;
        monsterBehaviour.appearance = appearance;
        monsterBehaviour.Setup();
    }
}
