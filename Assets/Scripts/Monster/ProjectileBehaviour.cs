using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour
{
    public float damage;
    public bool isGoo;

    public Vector2 direction;
    private float speed = 5f;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

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
        transform.rotation = Quaternion.Euler(0f, 0f, zRotation + 90f);
    }

    private float GetRotationFromVector(Vector2 direction)
    {
        float angleRadians = Mathf.Atan2(direction.y, direction.x);
        float angleDegrees = angleRadians * Mathf.Rad2Deg;
        return angleDegrees;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Player"))
        {
            collision.gameObject.GetComponent<PlayerBehaviour>().TakeDamage(damage);
        }
        else return;
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.StartsWith("Wall") && (gameObject.name.StartsWith("Harpoon") || isGoo))
        {
            if (isGoo)
            {
                GameObject.Find("StageManager").GetComponent<MonsterTable>().instantiateMonster(GameObject.Find("GameManager").GetComponent<GameManager>().GetLevel(), 0, transform.position);
                Loader loader = GameObject.Find("StageManager").GetComponent<Loader>();
                int roomID = loader.GetRoomIDClosestToPosition(transform.position);
                GameObject room = GameObject.Find("StageManager").GetComponent<StageGenerator>().rooms[roomID];
                room.GetComponent<MonsterSpawner>().createdMonster(0);
            }
            Destroy(gameObject);
        }
    }
}
