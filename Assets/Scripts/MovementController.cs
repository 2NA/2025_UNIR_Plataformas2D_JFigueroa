using UnityEngine;

public class MovementController : MonoBehaviour
{
    [SerializeField] float walkSpeed = 2f;
    [SerializeField] float jumpSpeed = 3f;

    Rigidbody2D rb2D;
    Animator animator;
    SpriteRenderer spriteRenderer;

    void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected Vector2 desiredMove = Vector2.zero;
    protected bool mustJump = false;
    protected bool mustPunch = false;
    protected virtual void Update()
    {
        rb2D.linearVelocityX = desiredMove.x * walkSpeed;

        if (desiredMove.x != 0f)
        {
            animator.SetBool("IsWalking", true);
        } else
        {
            animator.SetBool("IsWalking", false);
        }

        if (desiredMove.x < 0)
        {
            spriteRenderer.flipX = true;
        } else if (desiredMove.x > 0)
        {
            spriteRenderer.flipX = false;
        }

        if (mustJump)
        {
            mustJump = false;
            rb2D.linearVelocityY = jumpSpeed;
            animator.SetTrigger("PerformJump");
        }
        
        if (mustPunch)
        {
            mustPunch = false;
            animator.SetTrigger("PerformPunch");
        }
    }
}
