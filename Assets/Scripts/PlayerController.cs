/* Completed By:    Alexander Mundt - 101632886
 * Assignment:      Assignment 1
 * Class:           GAME-1017
 * Professor:       Ernie Burrows
 */
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float groundCheckDistance;

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
    }

    void Update()
    {
        //When we are InPlay...
        if (GameManager.Instance.GetGameState() == EGameState.InPlay)
        {
            //...check if the player is on the ground
            CheckGrounded();
        }
    }

    private void FixedUpdate()
    {
        //When we are InPlay...
        if (GameManager.Instance.GetGameState() == EGameState.InPlay)
        {
            //...move right
            MovePlayer();

            //Constant lateral movement
            Vector2 vel = rb.linearVelocity;
            vel.x = speed;

            rb.linearVelocity = vel;

            if (jumpPressed && isGrounded)
            {
                Jump();
            }

            jumpPressed = false;
        }
    }

    private void MovePlayer()
    {
        float distancePerFrame = speed * Time.deltaTime;
        transform.Translate(distancePerFrame, 0, 0);
    }

    private void CheckGrounded()
    {
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, groundLayer);
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
