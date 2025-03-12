using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 7f;
    public float climbSpeed = 3f; // Speed for climbing movement
    public float rotationSpeed = 5f;

    private Vector2 playerDirection;
    private Rigidbody rb;
    private bool isGrounded;
    private bool isClimbing = false; // Track if player is climbing
    public bool isPunching;
    private Animator animator;
    private Transform playerTransform;
    private Quaternion targetRotation;

    public int playerDamage = 1;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
        playerTransform = transform.GetChild(0);
        targetRotation = playerTransform.localRotation;
    }

    public void PlayerWalk(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            animator.SetBool("isWalking", true);
            Vector2 playerInput = ctx.ReadValue<Vector2>();
            playerDirection = playerInput; // Store full 2D movement (x & y)

            // Flip model when changing horizontal direction
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
        if (ctx.performed)
        {
            if (isClimbing)
            {
                // Jump off the building
                isClimbing = false;
                rb.useGravity = true;
                rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpForce, rb.linearVelocity.z);
                animator.SetBool("isJumping", true);
            }
            else if (isGrounded)
            {
                // Regular jump
                rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpForce, rb.linearVelocity.z);
                isGrounded = false;
                animator.SetBool("isJumping", true);
            }
        }
    }

    float nextswing = 0;

    public void PlayerPunch(InputAction.CallbackContext ctx)
    {
        if (ctx.performed && Time.time > nextswing)
        {
            isPunching = true;
            animator.SetTrigger("punchTrig");
            nextswing = Time.time + 0.5f;
        }
    }

    private void Update()
    {
        if (Time.time >= nextswing && isPunching)
        {
            isPunching = false;
        }

        if (isClimbing)
        {
            // Allow vertical movement while climbing
            Vector3 climbingMovement = new Vector3(playerDirection.x, playerDirection.y, 0) * climbSpeed * Time.deltaTime;
            transform.Translate(climbingMovement);
        }
        else
        {
            // Normal ground movement (left/right only)
            Vector3 movement = new Vector3(playerDirection.x, 0, 0) * moveSpeed * Time.deltaTime;
            transform.Translate(movement);
        }

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

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Building"))
        {
            Debug.Log("In Building");

            if (!isClimbing && !isGrounded)
            {
                // Latch onto building
                isClimbing = true;
                rb.linearVelocity = Vector3.zero;
                rb.useGravity = false;
                animator.SetBool("isJumping", false);
            }

            if (isPunching)
            {
                Debug.Log("Punched building");
                other.gameObject.GetComponent<BlockManager>().TakeDamage();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Building"))
        {
            // Exit climbing state when leaving the building
            isClimbing = false;
            rb.useGravity = true;
        }
    }
}
