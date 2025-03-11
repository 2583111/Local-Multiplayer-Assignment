using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 7f;
    public float rotationSpeed = 5f;
    private Vector2 playerDirection;
    private Rigidbody rb;
    private bool isGrounded;
    private bool isPunching;
    private Animator animator;
    private Transform playerTransform; // Reference to character model
    private Quaternion targetRotation;

    public int playerDamage = 1;

    private void Start()
    {
        rb = GetComponent<Rigidbody>(); // Ensure the GameObject has a Rigidbody
        animator = GetComponentInChildren<Animator>();
        playerTransform = transform.GetChild(0); // Assuming the model is the first child
        targetRotation = playerTransform.localRotation;
    }

    public void PlayerWalk(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            animator.SetBool("isWalking", true);
            Vector2 playerInput = ctx.ReadValue<Vector2>();
            playerDirection.x = playerInput.x;

            // Flip the model when changing direction
            if (playerDirection.x > 0)
            {
                targetRotation = Quaternion.Euler(0, -270, 0); // Face right
            }
            else if (playerDirection.x < 0)
            {
                targetRotation = Quaternion.Euler(0, -90, 0); // Face left
            }
        }
        else
        {
            animator.SetBool("isWalking", false);
            playerDirection = Vector2.zero;
        }
    }

    public void PlayerJump(InputAction.CallbackContext ctx)
    {
        if (ctx.performed && isGrounded)
        {

            animator.SetBool("isJumping", true);
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpForce, rb.linearVelocity.z);
            isGrounded = false;
        }
    }

    public void PlayerPunch(InputAction.CallbackContext ctx)
    {
        Debug.Log("Punch");

        isPunching = true;

        if (ctx.performed)
        {
            Debug.Log("punched");
            animator.SetTrigger("punchTrig");
        }

        isPunching = false;

        Debug.Log("Punch ended");
    }

    private void Update()
    {
        Vector3 movement = new Vector3(playerDirection.x, 0, 0) * moveSpeed * Time.deltaTime;
        transform.Translate(movement);

        playerTransform.localRotation = Quaternion.Lerp(playerTransform.localRotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            animator.SetBool("isJumping", false);
            isGrounded = true;
        }

    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Building")
        {
            Debug.Log("In Building");
            //other.gameObject.GetComponent<BlockManager>().TakeDamage();
        }
    }
}
