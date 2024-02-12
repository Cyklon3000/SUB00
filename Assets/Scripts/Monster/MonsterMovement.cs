using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMovement : MonoBehaviour
{
    private GameObject player;
    [SerializeField] public bool isMovingTowardsPlayer;
    [SerializeField] public bool isFleeingFromPlayer;
    [SerializeField] public float maxSpeed;
    private float currentSpeed = 0.0f;
    [SerializeField] public float timeToAccelerate;
    [SerializeField] public float timeToDecelerate;
    private Vector2 steeringVector = new Vector2(0.0f, 0.0f);
    private Vector2 lastDirection = new Vector2(0.0f, 0.0f);
    [SerializeField] public float range;
    private new Rigidbody2D rigidbody2D;
    [SerializeField] public bool isJumping;
    [SerializeField] public float jumpHeight;
    [SerializeField] public float jumpSpeed;
    [SerializeField] public bool isStretchMoving;
    [SerializeField] public float stretchFaktor;
    [SerializeField] public float stretchSpeed;
    [SerializeField] public bool isDashing;
    [SerializeField] public float dashForce;
    [SerializeField] public float linearDrag;
    [SerializeField] public float dashCooldown;
    private float lastDash;

    [HideInInspector]
    public int orientation = -1;

    private Transform appearance;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");

        rigidbody2D = GetComponent<Rigidbody2D>();
        appearance = transform.Find("Appearance");

        rigidbody2D.angularDrag = 0.0f;
        rigidbody2D.gravityScale = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        steeringVector = (Vector2)(player.transform.position - transform.position).normalized; // Player direction

        // Stop movement when close enough of far enough
        if (isMovingTowardsPlayer && (transform.position - player.transform.position).magnitude < range)
            steeringVector = Vector2.zero;
        else if (isFleeingFromPlayer && (transform.position - player.transform.position).magnitude > range)
            steeringVector = Vector2.zero;

        lastDirection = (steeringVector.sqrMagnitude > 0) ? steeringVector : lastDirection;

        if (steeringVector.sqrMagnitude == 0)
            currentSpeed = Mathf.Clamp(currentSpeed - (maxSpeed / timeToDecelerate) * Time.deltaTime, 0, maxSpeed); // Decelerate
        currentSpeed = Mathf.Clamp(currentSpeed + steeringVector.magnitude * (maxSpeed / timeToAccelerate) * Time.deltaTime, 0, maxSpeed); // Accelerate
    
        // jumping (pos.y) abs(sin)
        if (isJumping)
        {
            float yShift = jumpHeight * Mathf.Abs(Mathf.Sin(Time.time * jumpSpeed * Mathf.PI));
            appearance.transform.localPosition = (Vector3) Vector2.up * yShift;
        }

        // Stretching (scale) sin
        if (isStretchMoving)
        {
            float stretch = (stretchFaktor - 1) * (Mathf.Sin(Time.time * stretchSpeed * 2 * Mathf.PI) + 1) / 2;
            appearance.transform.localScale = Vector3.one + Vector3.one * stretch;
        }

        // Set view orientation
        if (isMovingTowardsPlayer || isFleeingFromPlayer)
        {
            if (lastDirection.y > Mathf.Abs(lastDirection.x)) // up
            {
                orientation = 0;
                //Debug.Log("UP");
            }
            if (lastDirection.x > Mathf.Abs(lastDirection.y)) // right
            {
                orientation = 1;
                //Debug.Log("RIGHT");
            }
            if (lastDirection.y < -Mathf.Abs(lastDirection.x)) // down
            {
                orientation = 2;
                //Debug.Log("DOWN");
            }
            if (lastDirection.x < -Mathf.Abs(lastDirection.y)) // left
            {
                orientation = 3;
                //Debug.Log("LEFT");
            }
        }
    }

    void FixedUpdate()
    {
        // Regular movement
        if (isMovingTowardsPlayer)
            rigidbody2D.MovePosition(rigidbody2D.position + (lastDirection * currentSpeed * Time.fixedDeltaTime));
        else if (isFleeingFromPlayer)
            rigidbody2D.MovePosition(rigidbody2D.position - (lastDirection * currentSpeed * Time.fixedDeltaTime));

        // Dash movement towards player
        if (!isDashing)
        {
            rigidbody2D.drag = 0;
            return;
        }
        rigidbody2D.drag = linearDrag;
        if (Time.time - lastDash <= dashCooldown)
            return;
        lastDash = Time.time;
        rigidbody2D.AddForce(steeringVector * dashForce);
    }
}
