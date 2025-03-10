using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    public float moveSpeedAgain = 5f;
    public float jumpForce = 7f; // Adjust for jump height
    private Vector2 cubeDirection;
    private Rigidbody rb;
    private bool isGrounded;

    private void Start()
    {
        rb = GetComponent<Rigidbody>(); // Ensure the GameObject has a Rigidbody
    }

    public void MoveTheCube(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            Vector2 playerInput = ctx.ReadValue<Vector2>();
            cubeDirection.x = playerInput.x;
        }
        else
        {
            cubeDirection = Vector2.zero;
        }
    }

    public void JumpTheCube(InputAction.CallbackContext ctx)
    {

        if (ctx.performed && isGrounded) // Only jump if grounded
        {
            Debug.Log("Jump");
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpForce, rb.linearVelocity.z);
            isGrounded = false; // Prevents multiple jumps
        }
    }

    private void Update()
    {
        Vector3 movement = new Vector3(cubeDirection.x, 0, 0) * moveSpeedAgain * Time.deltaTime;
        transform.Translate(movement);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if touching the ground
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}
