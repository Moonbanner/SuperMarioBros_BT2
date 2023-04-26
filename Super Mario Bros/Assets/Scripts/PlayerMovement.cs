using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private new Rigidbody2D rigidbody; //same name as a very old, obsolete property, hence the 'new'

    private Vector2 velocity;
    private float inputAxis;

    public float moveSpeed = 8.0f;
    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        HorizontalMovement();
    }

    private void HorizontalMovement()
    {
        inputAxis = Input.GetAxis("Horizontal");
        velocity.x = Mathf.MoveTowards(velocity.x, inputAxis * moveSpeed, moveSpeed * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        Vector2 position = rigidbody.position;
        position += velocity * Time.fixedDeltaTime;

        rigidbody.MovePosition(position);

    }
}
