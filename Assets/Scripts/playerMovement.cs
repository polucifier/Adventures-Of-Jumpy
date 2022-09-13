using UnityEngine;
//using UnityEngine.InputSystem;

public class playerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    private Rigidbody2D rb;
    private float direction;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        rb.velocity = new Vector2(direction * speed, rb.velocity.y);
    }
/*    private void OnMove(InputValue movementValue)
    {
        direction = movementValue.Get<float>();
        if(direction != 0) //flipping left-right
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * direction, transform.localScale.y, 1f);
        }
    }*/
    private void OnJump()
    {
        if(isGrounded())
        {
            rb.AddForce(Vector2.up * jumpForce);
        }
    }
    private bool isGrounded()
    {
        return Physics2D.BoxCast(GetComponent<BoxCollider2D>().bounds.center, GetComponent<BoxCollider2D>().bounds.size, 0, Vector2.down, 0.1f).collider != null;
    }
}
