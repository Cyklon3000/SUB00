using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterTable : MonoBehaviour
{
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
        GameObject monsterBase = GameObject.Find("Monster (Base)");
        GameObject monster = null;

        switch (level)
        {
        case 2:
            switch (monsterID)
            {
            case 0: // Pufferfish
                monster = Instantiate(monsterBase, position, Quaternion.Euler(0f, 0f, 0f));
                declareMonsterValues(monster.GetComponent<MonsterMovement>(),
                    true, false, 1.5f, 1.0f, 0.1f, 0.0f, true, 0.4f, 2.5f, false, 1.4f, 0.5f, false, 1300.0f, 3.5f, 2.5f);
                break;
            case 1: // Blobfish
                monster = Instantiate(monsterBase, position, Quaternion.Euler(0f, 0f, 0f));
                declareMonsterValues(monster.GetComponent<MonsterMovement>(),
                    false, true, 3.0f, 1.0f, 0.5f, 6.5f, false, 0.4f, 2.5f, true, 1.2f, 0.25f, false, 1300.0f, 3.5f, 2.5f);
                break;
            case 2: // Giant glow squid
                monster = Instantiate(monsterBase, position, Quaternion.Euler(0f, 0f, 0f));
                declareMonsterValues(monster.GetComponent<MonsterMovement>(),
                    false, false, 1.5f, 1.0f, 0.1f, 0f, false, 0.4f, 2.5f, false, 1.4f, 0.5f, false, 1300.0f, 3.5f, 2.5f);
                break;
            }
            break; 
        case 1:
            switch (monsterID)
            {
                case 0: // Glowing squid
                    monster = Instantiate(monsterBase, position, Quaternion.Euler(0f, 0f, 0f));
                    declareMonsterValues(monster.GetComponent<MonsterMovement>(),
                        true, false, 5.0f, 3.0f, 0.25f, 0.5f, false, 0.4f, 2.5f, true, 1.4f, 1.5f, false, 1300.0f, 3.5f, 2.5f);
                    break;
                case 1: // Mutated shark
                    monster = Instantiate(monsterBase, position, Quaternion.Euler(0f, 0f, 0f));
                    declareMonsterValues(monster.GetComponent<MonsterMovement>(),
                        false, false, 3.0f, 1.0f, 0.5f, 6.5f, false, 0.4f, 2.5f, false, 1.2f, 0.25f, true, 1300.0f, 3.5f, 2.5f);
                    break;
                case 2: // Giant angler fish
                    monster = Instantiate(monsterBase, position, Quaternion.Euler(0f, 0f, 0f));
                    declareMonsterValues(monster.GetComponent<MonsterMovement>(),
                        true, false, 1.5f, 1.0f, 0.1f, 0f, true, 0.75f, 0.75f, false, 1.4f, 0.5f, false, 1300.0f, 3.5f, 2.5f);
                    break;
            }
            break;
        case 0:
            switch (monsterID)
            {
                case 0: // Goo
                    monster = Instantiate(monsterBase, position, Quaternion.Euler(0f, 0f, 0f));
                    declareMonsterValues(monster.GetComponent<MonsterMovement>(),
                        false, false, 1.5f, 1.0f, 0.1f, 0.0f, false, 0.4f, 2.5f, true, 1.2f, 0.65f, false, 1300.0f, 3.5f, 2.5f);
                    break;
                case 1: // Infected diver
                    monster = Instantiate(monsterBase, position, Quaternion.Euler(0f, 0f, 0f));
                    declareMonsterValues(monster.GetComponent<MonsterMovement>(),
                        true, false, 7.0f, 2.0f, 0.5f, 4.0f, false, 0.4f, 2.5f, false, 1.2f, 0.25f, false, 1300.0f, 3.5f, 2.5f);
                    break;
                case 2: // Giant goo Monster
                    monster = Instantiate(monsterBase, position, Quaternion.Euler(0f, 0f, 0f));
                    declareMonsterValues(monster.GetComponent<MonsterMovement>(),
                        false, false, 1.5f, 1.0f, 0.1f, 0f, false, 0.4f, 2.5f, true, 1.1f, 0.65f, false, 1300.0f, 3.5f, 2.5f);
                    break;
            }
            break;
        }

        return monster;
    }

    private void declareMonsterValues(MonsterMovement monster,  bool isMovingTowardsPlayer, bool isFleeingFromPlayer, 
        float maxSpeed, float timeToAccelerate, float timeToDecelerate, float range, bool isJumping, float jumpHeight, 
        float jumpSpeed, bool isStretchMoving, float stretchFaktor, float stretchSpeed, bool isDashing, float dashForce, 
        float linearDrag, float dashCooldown)
    {
        monster.isMovingTowardsPlayer = isMovingTowardsPlayer;
        monster.isFleeingFromPlayer = isFleeingFromPlayer;
        monster.maxSpeed = maxSpeed;
        monster.timeToAccelerate = timeToAccelerate;
        monster.timeToDecelerate = timeToDecelerate;
        monster.range = range;
        monster.isJumping = isJumping;
        monster.jumpHeight = jumpHeight;
        monster.jumpSpeed = jumpSpeed;
        monster.isStretchMoving = isStretchMoving;
        monster.stretchFaktor = stretchFaktor;
        monster.stretchSpeed = stretchSpeed;
        monster.isDashing = isDashing;
        monster.dashForce = dashForce;
        monster.linearDrag = linearDrag;
        monster.dashCooldown = dashCooldown;
    }
}
