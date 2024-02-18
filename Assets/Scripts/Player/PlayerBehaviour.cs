using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBehaviour : MonoBehaviour
{
    public float health = 250f;

    private GameObject weapon;

    private Color damageIndicationColor = new Color(1f, 0.5f, 0.5f);
    private float damageIndicationStrength = 0f;
    private float damageIndicationDuration = 0.4f;

    private AudioManager audioManager;

    // Start is called before the first frame update
    void Start()
    {
        weapon = GameObject.Find("Weapon");
        weapon.transform.parent = transform;
        weapon.transform.localPosition = Vector3.zero;
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        // Turns player according to mouse position
        Vector3 mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 0f));
        Vector3 direction = (mousePos - transform.position).normalized;
        transform.rotation = Quaternion.Euler(0f, 0f, GetAngleInDegrees(direction) - 90f);

        if (damageIndicationStrength > 0)
        {
            damageIndicationStrength = Mathf.Clamp01(damageIndicationStrength - Time.deltaTime / damageIndicationDuration);
            GetComponent<SpriteRenderer>().color = Color.Lerp(Color.white, damageIndicationColor, damageIndicationStrength);
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        IndicateDamage();

        audioManager.playerHit.Play();

        if (health <= 0)
        {
            health = 0;
            GameOver();
        }
        Debug.Log($"Took {damage} damage; {health} left");
    }

    public void ResetHealth()
    {
        health = 250f;
        IndicateDamage(false);
    }

    private void IndicateDamage(bool isRedGlow = true)
    {
        // Red glow and health bar
        Slider slider = GameObject.Find("HealthBarSlider").GetComponent<Slider>();
        slider.value = health / 250f;
        if (isRedGlow )
        {
            damageIndicationStrength = 1f;
        }
    }

    private void GameOver()
    {
        Debug.Log("Game Over! (Player died)");
        GameObject.Find("GameManager").GetComponent<GameManager>().EndAttempt(true);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!collision.gameObject.name.StartsWith("Item")) return;
        collision.GetComponent<Item>().PickItemUp();
    }

    private static float GetAngleInDegrees(Vector2 vector)
    {
        float angleInRadians = Mathf.Atan2(vector.y, vector.x);
        float angleInDegrees = angleInRadians * Mathf.Rad2Deg;
        return angleInDegrees;
    }
}
