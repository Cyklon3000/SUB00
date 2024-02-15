using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public float health = 100f;

    private GameObject weapon;

    // Start is called before the first frame update
    void Start()
    {
        weapon = GameObject.Find("Weapon");
        weapon.transform.parent = transform;
        weapon.transform.localPosition = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        // Turns weapon according to mouse position
        Vector3 mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 0f));
        Vector3 direction = (mousePos - transform.position).normalized;
        weapon.transform.rotation = Quaternion.Euler(0f, 0f, GetAngleInDegrees(direction) - 90f);
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        IndicateDamage();

        if (health <= 0)
        {
            health = 0;
            GameOver();
        }
        Debug.Log($"Took {damage} damage; {health} left");
    }

    private void IndicateDamage()
    {
        // Red glow and health bar
    }

    private void GameOver()
    {
        Debug.Log("Game Over!");
        // GameOver initiation
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
