using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public float health = 100f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        IndicateDamage();

        if (health < 0)
        {
            health = 0;
            GameOver();
        }
    }

    private void IndicateDamage()
    {
        // Red glow and health bar
    }

    private void GameOver()
    {
        // GameOver initiation
    }
}
