using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponStats : MonoBehaviour
{
    public float tickDamage;
    public float tickDuration;
    public bool isNapalm;

    public bool isActive = false;

    private HashSet<MonsterBehaviour> monsters = new HashSet<MonsterBehaviour>();
    private HashSet<MonsterBehaviour> napalmMonsters = new HashSet<MonsterBehaviour>();

    public void Activate()
    {
        if (isActive) return;
        GameObject flame = transform.Find("Flame").gameObject;
        //flame.SetActive(true);
        flame.GetComponent<ParticleSystem>().Play();
        isActive = true;
    }

    public void Deactivate()
    {
        if (!isActive) return;
        GameObject flame = transform.Find("Flame").gameObject;
        //flame.SetActive(false);
        flame.GetComponent<ParticleSystem>().Stop();
        isActive = false;
    }

    public void Enable()
    {
        GetComponent<SpriteRenderer>().enabled = true;
    }

    public void Disable()
    {
        //Debug.Log($"Disabled {gameObject.name}'s Sprite");
        GetComponent<SpriteRenderer>().enabled = false;
        isActive = true;
        Deactivate();
    }

    public void executeTick()
    {
        //Debug.Log($"Executing tick for {monsters.Count} monsters");
        foreach (MonsterBehaviour monster in monsters)
        {
            monster.takeDamage(tickDamage);
        }
        monsters = new HashSet<MonsterBehaviour>();
    }

    public void executeNapalmTick()
    {
        foreach (MonsterBehaviour monster in napalmMonsters)
        {
            if (Random.Range(0f, 1f) > 0.33f) continue;
            monster.takeDamage(tickDamage);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (isActive)
        {
            if (collision.gameObject.name.EndsWith("GlowSquidLegs(Clone)"))
                monsters.Add(collision.transform.parent.gameObject.GetComponent<MonsterBehaviour>());
            else if (collision.gameObject.name.EndsWith("AnglerFishRod(Clone)"))
                monsters.Add(collision.transform.parent.parent.gameObject.GetComponent<MonsterBehaviour>());
            else
                monsters.Add(collision.gameObject.GetComponent<MonsterBehaviour>());
        }
        if (isActive && isNapalm)
        {
            napalmMonsters.Add(collision.gameObject.GetComponent<MonsterBehaviour>());
            collision.gameObject.GetComponent<MonsterBehaviour>().AddNapalmParticles();
        }
    }
}
