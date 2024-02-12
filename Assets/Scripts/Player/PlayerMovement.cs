using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float maxSpeed;
    private float currentSpeed = 0.0f;
    public float timeToAccelerate;
    public float timeToDecelerate;

    private Vector2 inputVector = new Vector2(0.0f, 0.0f);
    private Vector2 lastDirection = new Vector2(0.0f, 0.0f);
    private new Rigidbody2D rigidbody2D;

    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();

        rigidbody2D.angularDrag = 0.0f;
        rigidbody2D.gravityScale = 0.0f;
    }

    void Update()
    {
        inputVector = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        lastDirection = (inputVector.sqrMagnitude > 0) ? inputVector : lastDirection;
        if (inputVector.sqrMagnitude == 0)
            currentSpeed = Mathf.Clamp(currentSpeed - (maxSpeed / timeToDecelerate) * Time.deltaTime, 0, maxSpeed); // Decelerate
        currentSpeed = Mathf.Clamp(currentSpeed + inputVector.magnitude * (maxSpeed / timeToAccelerate) * Time.deltaTime, 0, maxSpeed);
    }

    void FixedUpdate()
    {
        // Debug.Log(currentSpeed);
        rigidbody2D.MovePosition(rigidbody2D.position + (lastDirection * currentSpeed * Time.fixedDeltaTime));
    }
}