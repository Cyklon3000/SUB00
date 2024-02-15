using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponStats : MonoBehaviour
{
    public float baseTickDamage;
    [HideInInspector] public float tickDamage;
    public float baseTickDuration;
    [HideInInspector] public float tickDuration;
    [HideInInspector] public bool isNapalm = false;

    public bool isActive = false;

    private HashSet<MonsterBehaviour> monsters = new HashSet<MonsterBehaviour>();
    private HashSet<MonsterBehaviour> napalmMonsters = new HashSet<MonsterBehaviour>();

    public void Activate()
    {
        if (isActive) return;
        ParticleSystem flame = transform.Find("Flame").gameObject.GetComponent<ParticleSystem>();
        flame.Play();
        isActive = true;
    }

    public void Deactivate()
    {
        if (!isActive) return;
        ParticleSystem flame = transform.Find("Flame").gameObject.GetComponent<ParticleSystem>();
        flame.Stop();
        isActive = false;
    }

    public void Enable()
    {
        GetComponent<SpriteRenderer>().enabled = true;
    }

    public void Disable()
    {
        GetComponent<SpriteRenderer>().enabled = false;
    }

    public void executeTick()
    {
        //Debug.Log($"Executing tick for {monsters.Count} monsters");
        foreach (MonsterBehaviour monster in monsters)
        {
            monster.takeDamage(tickDamage);
        }
        monsters = new HashSet<MonsterBehaviour>();
        
        if (!isNapalm) return;
        if (Random.Range(0f, 1f) > .33f) return;
        Invoke("executeNapalmTick", tickDuration / 2);
    }

    private void executeNapalmTick()
    {
        foreach (MonsterBehaviour monster in napalmMonsters)
        {
            monster.takeDamage(tickDamage);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (isActive)
        {
            if (collision.gameObject.name.StartsWith("GlowSquidLegs"))
                monsters.Add(collision.transform.parent.gameObject.GetComponent<MonsterBehaviour>());
            else
                monsters.Add(collision.gameObject.GetComponent<MonsterBehaviour>());
        }
        if (isActive && isNapalm)
            napalmMonsters.Add(collision.gameObject.GetComponent<MonsterBehaviour>());
        // Add Napalm Particles to monsters
    }
}
