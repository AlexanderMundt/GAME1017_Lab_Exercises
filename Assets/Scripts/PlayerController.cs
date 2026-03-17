/* Completed By:    Alexander Mundt - 101632886
 * Assignment:      Assignment 1
 * Class:           GAME-1017
 * Professor:       Ernie Burrows
 */

using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speedLimit;
    [SerializeField] private float jumpForce;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float groundCheckDistance;

    [Tooltip("Represents the lowest position that player can go in the y axis before dying")]
    [SerializeField] private float lowerYDeathPlane;

    private Vector3 startPosition;
    private Rigidbody2D rb;
    private bool jumpPressed = false;
    private bool isGrounded = false;

    public void Initialize()
    {
        //Save the player's starting position
        startPosition = transform.position;

        //Turn on gravity
        rb = GetComponent<Rigidbody2D>();
        rb.simulated = true;

        //Set initial speed
        rb.linearVelocity = new Vector2(speedLimit, 0.0f);
    }

    void Update()
    {
        //When we are InPlay...
        if (GameManager.Instance.GetGameState() == EGameState.InPlay)
        {
            //...check if the player is on the ground
            CheckGrounded();

            //Check if the player has fallen
            CheckLowerYDeathPlane();
        }
    }

    private void FixedUpdate()
    {
        //When we are InPlay...
        if (GameManager.Instance.GetGameState() == EGameState.InPlay)
        {
            //If the player is stuck then gameover
            if (rb.linearVelocity.magnitude == 0)
            {
                GameManager.Instance.GameOver();
            }

            //Movement stuff
            rb.AddForce(1.0f * Vector2.right, ForceMode2D.Impulse);

            //Clamp only the x axis movement
            Vector2 currentLinVel = rb.linearVelocity;
            currentLinVel.x = Mathf.Clamp(currentLinVel.x, 0.0f, speedLimit);
            rb.linearVelocity = currentLinVel;

            //Jump stuff
            if (jumpPressed && isGrounded)
            {
                Jump();
            }

            jumpPressed = false;
        }
    }

    private void CheckGrounded()
    {
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, groundLayer);
    }

    private void CheckLowerYDeathPlane()
    {
        if (transform.position.y < lowerYDeathPlane)
        {
            GameManager.Instance.GameOver();
        }
    }

    private void Jump()
    {
        //Reset vertical speed for consistent jump height
        Vector2 vel = rb.linearVelocity;
        vel.y = 0.0f;

        rb.linearVelocity = vel;

        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    //Called by the input system when the jump action is triggered
    public void OnJump()
    {
        jumpPressed = true;
    }

    public void ResetPlayer()
    {
        //Load the player's starting position
        transform.SetPositionAndRotation(startPosition, Quaternion.identity);

        //Turn off gravity
        rb.linearVelocity = Vector2.zero;
        rb.simulated = false;
        jumpPressed = false;
    }
}
