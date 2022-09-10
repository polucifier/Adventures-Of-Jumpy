using UnityEngine;
using UnityEngine.InputSystem;

public class playerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    private Rigidbody2D rb;
    private float direction;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void OnMove(InputValue movementValue)
    {
        direction = movementValue.Get<float>();
        if (direction != 0) //flipping left-right
        {
            transform.localScale = new Vector3(6.25f * direction, 6.25f, 1f);
        }
    }
    private void OnJump()
    {
        rb.AddForce(Vector2.up * jumpForce);
    }
    private void FixedUpdate()
    {
        rb.velocity = new Vector2(direction * speed, rb.velocity.y);
    }
}
