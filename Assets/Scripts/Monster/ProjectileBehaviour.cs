using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour
{
    public float damage;
    public bool isMonster;

    public Vector2 direction;
    private float speed = 5f;
    private float decelerationSpeed = 1f;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!isMonster) return;
        speed = speed - (speed / decelerationSpeed) * Time.fixedDeltaTime;
        if (speed > 0) return;
        rb.velocity = Vector2.zero;
        enabled = false;
    }

    void FixedUpdate()
    {
        if (direction == null) return;
        if (rb == null) rb = GetComponent<Rigidbody2D>();

        rb.MovePosition((Vector2) transform.position + direction * speed * Time.fixedDeltaTime);
    }

    public void Shoot(Vector3 direction)
    {
        this.direction = direction;
        float zRotation = GetRotationFromVector(direction);
        transform.rotation = Quaternion.Euler(0f, 0f, zRotation);
    }

    private float GetRotationFromVector(Vector2 direction)
    {
        float angleRadians = Mathf.Atan2(direction.y, direction.x);
        float angleDegrees = angleRadians * Mathf.Rad2Deg;
        return angleDegrees;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.StartsWith("Wall"))
        {
            rb.velocity = Vector2.zero;
            enabled = false;
        }
        else if (collision.gameObject.name.Equals("Player"))
        {
            collision.gameObject.GetComponent<PlayerBehaviour>().TakeDamage(damage);
        }
        else return;
        Destroy(gameObject);
    }
}
