using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMovement : MonoBehaviour
{
    private GameObject player;
    [SerializeField] private bool isMovingTowardsPlayer;
    [SerializeField] private bool isFleeingFromPlayer;
    [SerializeField] private float maxSpeed;
    private float currentSpeed = 0.0f;
    [SerializeField] private float timeToAccelerate;
    [SerializeField] private float timeToDecelerate;
    private Vector2 steeringVector = new Vector2(0.0f, 0.0f);
    private Vector2 lastDirection = new Vector2(0.0f, 0.0f);
    [SerializeField] private float range;
    private new Rigidbody2D rigidbody2D;
    [SerializeField] private bool isJumping;
    [SerializeField] private float jumpHeight;
    [SerializeField] private float jumpSpeed;
    [SerializeField] private bool isStretchMoving;
    [SerializeField] private float stretchFaktor;
    [SerializeField] private float stretchSpeed;
    [SerializeField] private bool isDashing;
    [SerializeField] private float dashForce;
    [SerializeField] private float dashCooldown;
    private float lastDash;

    private Transform appearance;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");

        rigidbody2D = GetComponent<Rigidbody2D>();

        rigidbody2D.angularDrag = 0.0f;
        rigidbody2D.gravityScale = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        steeringVector = (Vector2)(transform.position - player.transform.position).normalized; // Player direction

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
            appearance.transform.position = (Vector3) Vector2.up * yShift;
        }

        // Stretching (scale) sin
        if (isStretchMoving)
        {
            float stretch = stretchFaktor * Mathf.Sin(Time.time * stretchSpeed * 2 * Mathf.PI);
            appearance.transform.localScale = Vector3.one * (1 + stretch);
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
            return;
        if (Time.time - lastDash <= dashCooldown)
            return;
        lastDash = Time.time;
        rigidbody2D.AddForce(steeringVector * dashForce);
    }
}
