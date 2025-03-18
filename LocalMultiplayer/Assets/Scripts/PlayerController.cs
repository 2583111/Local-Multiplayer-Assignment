using System.Collections;
using TMPro;
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
    public bool isGrounded;
    public bool isClimbing = false; // Track if player is climbing
    public bool isPunching;
    private Animator animator;
    private Transform playerTransform;
    public Quaternion targetRotation;

    public int playerDamage = 1;
    public float playerScore = 0;
    public int whichPlayer;

    public float playerZPos;



    public TextMeshProUGUI Score;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
        playerTransform = transform.GetChild(0);
        targetRotation = playerTransform.localRotation;

        Score.text = playerScore.ToString();

    }

    public void PlayerWalk(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            animator.SetBool("isWalking", true);
            Vector2 playerInput = ctx.ReadValue<Vector2>();
            playerDirection = playerInput; // Store full 2D movement (x & y)

            if (isClimbing)
            {
                targetRotation = Quaternion.Euler(0, 360, 0);
            }
            else
            {

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
                animator.SetBool("isClimbing", false);
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
            nextswing = Time.time + 0.01f;
        }
    }

    public void updateScore ()
    {
        Score.text = playerScore.ToString();
    }

    private void Update()
    {
        if (Time.time >= nextswing && isPunching)
        {
            isPunching = false;
        }

        if (isClimbing)
        {
           

            if (isClimbing)
            {
                Vector3 climbingMovement = new Vector3(playerDirection.x, playerDirection.y, 0) * climbSpeed * Time.deltaTime;
                transform.Translate(climbingMovement);

                if (playerDirection != Vector2.zero)
                {
                    animator.speed = 1f; // Normal animation speed when moving
                }
                else
                {
                    animator.speed = 0f; // Freeze the animation when not moving
                }
            }
            else
            {
                animator.speed = 1f; // Reset the animation speed when not climbing
            }


            targetRotation = Quaternion.Euler(0, 360, 0);
        }
        else
        {
            // Normal ground movement (left/right only)
            Vector3 movement = new Vector3(playerDirection.x, 0, 0) * moveSpeed * Time.deltaTime;
            transform.Translate(movement);
        }

        playerTransform.localRotation = Quaternion.Lerp(playerTransform.localRotation, targetRotation, rotationSpeed * Time.deltaTime);

        if (isGrounded)
        {
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, playerZPos);
        }
       

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
            //Debug.Log("In Building");

            if (!isClimbing && !isGrounded)
            {
                // Latch onto building
                isClimbing = true;

                gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, other.transform.position.z-1.8f);

                rb.linearVelocity = Vector3.zero;
                rb.useGravity = false;
                animator.SetBool("isJumping", false);
                animator.SetBool("isClimbing", true);
               

            }

            if (isPunching)
            {
                //Debug.Log("Punched building");
                other.gameObject.GetComponent<BlockManager>().TakeDamage(whichPlayer);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Building"))
        {
            // Exit climbing state when leaving the building
            isClimbing = false;
            animator.SetBool("isClimbing", false);
            rb.useGravity = true;
        }
    }
}
