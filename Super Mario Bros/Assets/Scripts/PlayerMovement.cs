using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private new Rigidbody2D rigidbody; //same name as a very old, obsolete property, hence the 'new'
    private new Camera camera;         //same problem

    private Vector2 velocity;
    private float inputAxis;

    public float moveSpeed = 8.0f;
    public float maxJumpHeight = 5.0f;
    public float maxJumpTime = 1.0f;
    public float jumpForce => (2f * maxJumpHeight) / (maxJumpTime / 2f);
    public float gravity => (-2f * maxJumpHeight) / Mathf.Pow((maxJumpTime / 2f), 2);

    public bool grounded { get; private set; }
    public bool jumping { get; private set; }

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        camera = Camera.main;
    }

    private void Update()
    {
        HorizontalMovement();

        grounded = rigidbody.Raycast(Vector2.down);

        if (grounded)
        {
            GroundedMovement();
        }

        ApplyGravity();
    }

    private void HorizontalMovement()
    {
        inputAxis = Input.GetAxis("Horizontal");
        velocity.x = Mathf.MoveTowards(velocity.x, inputAxis * moveSpeed, moveSpeed * Time.deltaTime);

        if (rigidbody.Raycast(Vector2.right * velocity.x))
        {
            velocity.x = 0f;
        }
    }

    private void GroundedMovement()
    {
        velocity.y = Mathf.Max(velocity.y, 0f); //prevents y velocity from building up too much while Mario is grounded
        jumping = velocity.y > 0f;

        if (Input.GetButtonDown("Jump"))
        {
            velocity.y = jumpForce;
            jumping = true;
        }
    }

    private void ApplyGravity()
    {
        bool falling = velocity.y < 0f || !Input.GetButton("Jump");     //Handles the gravity to make Mario jump higher
        float multiplier = falling ? 2f : 1f;                           //if the player keep holding down the jump button

        velocity.y += gravity * multiplier * Time.deltaTime;
        velocity.y = Mathf.Max(velocity.y, gravity / 2f); //Terminal velocity, prevents Mario from falling TOO FAST
    }

    private void FixedUpdate()
    {
        Vector2 position = rigidbody.position;
        position += velocity * Time.fixedDeltaTime;

        Vector2 leftEdge = camera.ScreenToWorldPoint(Vector2.zero);
        Vector2 rightEdge = camera.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        position.x = Mathf.Clamp(position.x, leftEdge.x + 0.5f, rightEdge.x - 0.5f);

        rigidbody.MovePosition(position);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer != LayerMask.NameToLayer("PowerUp"))
        {
            if (transform.DotTest(collision.transform, Vector2.up))
            {
                velocity.y = 0f;
            }
        }
    }
}
